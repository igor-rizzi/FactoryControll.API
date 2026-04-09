# FactoryControll Web — Frontend Design Spec

**Data:** 2026-04-08  
**Status:** Aprovado  

---

## Contexto

A FactoryControll API é um sistema de gestão industrial com módulos de colaboradores, reembolsos e administração. Atualmente existe apenas a camada de API (ASP.NET Core, clean architecture). Este documento especifica o frontend que se comunicará com essa API via HTTP — sem acesso direto ao banco de dados.

---

## Stack Tecnológica

| Camada | Tecnologia |
|---|---|
| Framework | Next.js (App Router, TypeScript) |
| UI Components | shadcn/ui |
| Estilização | Tailwind CSS |
| Tema dark/light | next-themes |
| Autenticação | NextAuth.js v5 (Auth.js) |
| Estado servidor | TanStack Query v5 (React Query) |
| HTTP Client | Axios |
| Formulários | react-hook-form + zod |
| Repositório | Separado da API (novo repo: `factorycontroll-web`) |

---

## Arquitetura

### Visão geral

O frontend é uma aplicação Next.js App Router que consome exclusivamente a FactoryControll API via HTTP. Nenhum acesso direto ao banco de dados é permitido.

```
Browser
  └── Next.js App Router
        ├── NextAuth.js v5        → gerencia sessão (JWT em cookie HTTP-only)
        ├── middleware.ts         → protege rotas autenticadas
        ├── TanStack Query        → cache e sincronização de dados
        └── Axios (lib/api/)      → cliente HTTP com interceptors
              └── FactoryControll.API (localhost:XXXX)
```

### Autenticação e refresh token

1. Usuário faz login com email + senha
2. NextAuth chama `POST /api/Autenticacao/login` via credentials provider
3. JWT + refresh token ficam em cookie HTTP-only (nunca expostos ao JS)
4. Interceptor Axios injeta `Authorization: Bearer <jwt>` em toda request
5. Interceptor 401: chama `POST /api/Autenticacao/refresh-token`, atualiza cookie, refaz a request original
6. Falha no refresh: redireciona para `/login`

> **Novo endpoint na API:** `POST /api/Autenticacao/refresh-token` será adicionado à FactoryControll.API como parte deste projeto. Body: `{ refreshToken: string }`. Response: `{ token, refreshToken, expiration }`.

### Controle de acesso por role

| Role | Acesso |
|---|---|
| Administrador | Todos os módulos |
| Analista Financeiro | Dashboard + Reembolsos (aprovar/reprovar) |
| Colaborador | Dashboard + Meus reembolsos (criar/visualizar) |

O middleware Next.js lê o role da sessão e redireciona para `/unauthorized` se o usuário tentar acessar rota fora do seu nível.

---

## Layout

- **Sidebar colapsável**: no estado colapsado exibe apenas ícones; expandida exibe ícone + label. Itens de menu filtrados por role.
- **Topbar**: breadcrumb da rota atual, botão de toggle dark/light, avatar com dropdown (nome, logout).
- O layout da sidebar/topbar só existe dentro do route group `(dashboard)` — páginas de auth não herdam esse layout.

---

## Estrutura de Pastas

```
factorycontroll-web/
├── app/
│   ├── (auth)/
│   │   ├── login/page.tsx
│   │   ├── recuperar-senha/page.tsx
│   │   └── redefinir-senha/page.tsx
│   ├── (dashboard)/
│   │   ├── layout.tsx              ← Sidebar + Topbar
│   │   ├── page.tsx                ← Dashboard (métricas)
│   │   ├── colaboradores/
│   │   │   ├── page.tsx            ← listagem paginada
│   │   │   ├── novo/page.tsx
│   │   │   └── [id]/page.tsx
│   │   ├── reembolsos/
│   │   │   ├── page.tsx
│   │   │   ├── novo/page.tsx
│   │   │   └── [id]/page.tsx
│   │   ├── usuarios/
│   │   │   ├── page.tsx
│   │   │   └── novo/page.tsx
│   │   └── administracao/
│   │       ├── cargos/page.tsx
│   │       ├── funcoes/page.tsx
│   │       └── tipos-despesa/page.tsx
│   ├── api/auth/[...nextauth]/route.ts
│   └── layout.tsx                  ← root (ThemeProvider)
├── components/
│   ├── ui/                         ← shadcn components
│   ├── layout/
│   │   ├── Sidebar.tsx
│   │   ├── Topbar.tsx
│   │   └── ThemeToggle.tsx
│   ├── shared/
│   │   ├── DataTable.tsx           ← tabela genérica reutilizável
│   │   └── PageHeader.tsx
│   ├── colaboradores/
│   ├── reembolsos/
│   ├── usuarios/
│   └── administracao/
├── lib/
│   ├── api/
│   │   ├── axios.ts                ← instância Axios + interceptors
│   │   ├── auth.service.ts
│   │   ├── colaboradores.service.ts
│   │   ├── reembolsos.service.ts
│   │   ├── usuarios.service.ts
│   │   └── admin.service.ts
│   ├── auth.ts                     ← config NextAuth
│   └── query-client.ts
├── middleware.ts                   ← proteção de rotas
├── auth.ts                         ← NextAuth exportado
└── .env.local                      ← NEXTAUTH_SECRET, API_BASE_URL
```

---

## Rotas

### Públicas
| Rota | Descrição |
|---|---|
| `/login` | Login com email + senha |
| `/recuperar-senha` | Solicitar reset de senha por email |
| `/redefinir-senha?token=` | Definir nova senha via token do email |

### Autenticadas (todos os roles)
| Rota | Descrição |
|---|---|
| `/` | Dashboard com métricas gerais |
| `/reembolsos` | Lista de reembolsos (filtrada por role) |
| `/reembolsos/novo` | Criar novo reembolso + upload de comprovante |
| `/reembolsos/[id]` | Detalhe, aprovar/reprovar (Analista/Admin) |

### Administrador only
| Rota | Descrição |
|---|---|
| `/colaboradores` | Listagem + CRUD |
| `/colaboradores/novo` | Novo colaborador |
| `/colaboradores/[id]` | Editar colaborador |
| `/usuarios` | Listagem + CRUD |
| `/usuarios/novo` | Novo usuário |
| `/administracao/cargos` | CRUD de cargos |
| `/administracao/funcoes` | CRUD de funções |
| `/administracao/tipos-despesa` | CRUD de tipos de despesa |

---

## Padrões de Componentes

### Páginas de listagem (CRUD)
- `PageHeader` com título e botão "Novo"
- `DataTable` genérica com paginação (alinhada ao padrão `?page=&pageSize=` da API)
- Ações por linha: Editar (ícone lápis) e Deletar (ícone lixeira com dialog de confirmação)
- Toast de sucesso/erro após cada operação

### Formulários (criar/editar)
- `shadcn/ui Form` + `react-hook-form` + `zod`
- Validação no cliente antes do submit
- Botão com estado loading durante a chamada à API
- Redirect para listagem após sucesso

### Reembolsos (comportamento especial)
- Campo de upload de comprovante (PDF/imagem)
- Badge de status colorido: Pendente (amarelo), Aprovado (verde), Reprovado (vermelho), Pago (azul)
- Botões Aprovar/Reprovar visíveis apenas para Analista Financeiro e Administrador
- Preview inline do comprovante na página de detalhe

---

## Fluxo de Dados

```
Page.tsx
  → useQuery(queryKey, () => service.listar(params))   ← TanStack Query
    → axios.get('/api/[endpoint]/listar', { params })  ← interceptor injeta Bearer
      → FactoryControll.API                            ← resposta paginada

Mutation (criar/editar/deletar):
  → useMutation(() => service.inserir(data))
    → onSuccess: queryClient.invalidateQueries(queryKey)
      → lista recarregada automaticamente
```

---

## Tratamento de Erros e Estados

| Situação | Comportamento |
|---|---|
| Loading | Skeleton nos cards/tabelas |
| Erro de API | Toast vermelho com mensagem retornada pela API |
| 401 (token expirado) | Interceptor Axios tenta refresh; se falhar → redirect `/login` |
| 403 (sem permissão) | Redirect para `/unauthorized` |
| Sucesso | Toast verde + invalidate query para atualizar lista |

---

## Mudanças na FactoryControll.API

Além do frontend, uma alteração é necessária na API:

1. **Novo endpoint:** `POST /api/Autenticacao/refresh-token`
   - Input: `{ refreshToken: string }`
   - Output: `{ token: string, refreshToken: string, expiration: DateTime }`
   - A API precisa gerar e armazenar refresh tokens na tabela de usuários (ou cache)

2. **CORS:** atualizar origem permitida de `http://localhost:5173` para `http://localhost:3000` (porta padrão Next.js)

---

## Verificação (como testar end-to-end)

1. Iniciar FactoryControll.API localmente (`dotnet run`)
2. Iniciar o frontend (`npm run dev` na pasta `factorycontroll-web`)
3. Acessar `http://localhost:3000` — deve redirecionar para `/login`
4. Fazer login com usuário Administrador → deve ver sidebar completa
5. Fazer login com Colaborador → sidebar deve ocultar módulos Admin
6. Criar um reembolso com upload de comprovante
7. Logar como Analista Financeiro → aprovar o reembolso
8. Aguardar 2h ou forçar expiração do JWT → verificar que o refresh é transparente
9. Testar toggle dark/light → preferência deve persistir entre reloads
