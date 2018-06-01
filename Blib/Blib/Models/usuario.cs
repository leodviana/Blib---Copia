using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blib.Models
{
    public partial class usuario
    {
        public long isn_usuario { get; set; }
        public long isn_pessoa { get; set; }
        public string flg_super_usuario { get; set; }
        public string flg_desenvolvedor { get; set; }
        public string flg_profissional { get; set; }
        public string cod_senha { get; set; }
        public string cod_latitude { get; set; }
        public string cod_longitude { get; set; }
        public string dsc_nome_usuario { get; set; }
        public string dsc_email { get; set; }
        public int isn_status_usuario { get; set; }
        public System.DateTime dat_cadastro { get; set; }
        public long isn_usuario_cadastro { get; set; }
        public System.DateTime dat_alteracao { get; set; }
        public long isn_usuario_alteracao { get; set; }
        public long isn_usuario_pai { get; set; }
        public int isn_status_usuario_pai { get; set; }
    }
}
