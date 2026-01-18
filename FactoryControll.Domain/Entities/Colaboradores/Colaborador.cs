using FactoryControll.Domain.Common;
using FactoryControll.Domain.Entities.Administracao;
using FactoryControll.Domain.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryControll.Domain.Entities.Colaboradores
{
    public class Colaborador : Entity
    {
        #region Properties

        [Required]
        public required string Cpf { get; set; }

        [Required]
        public required string Nome { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        public DateTime DataAdmissao { get; set; }

        public DateTime? DataRescisao { get; set; }

        [Required]
        public required string Email { get; set; }

        [NotMapped]
        public bool Ativo => !DataRescisao.HasValue || DataRescisao.Value.Date > DateTime.Now.Date;

        #endregion Properties

        #region Foreign Keys

        [Required]
        public long CargoId { get; set; }

        [Required]
        public long FuncaoId { get; set; }


        public long? UsuarioId { get; set; }

        #endregion Foreign Keys

        #region Related Properties

        public Cargo Cargo { get; set; }

        public Funcao Funcao { get; set; }

        public Usuario Usuario { get; set; }

        #endregion Related Properties
    }
}
