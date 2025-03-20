using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzariaEstelarClass
{
    public class Tipos
    {
        public int Id { get; set; }
        public string Sigla { get; set; }
        public string Rotulo { get; set; }


        public Tipos() { }

        public Tipos(int id, string sigla, string rotulo)
        {
            Id = id;
            Sigla = sigla;
            Rotulo = rotulo;
        }
        public void Inserir()
        {
            var cmd = Banco.Abrir();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_tipos_insert";
            cmd.Parameters.AddWithValue("spsigla", Sigla);
            cmd.Parameters.AddWithValue("sprotulo", Rotulo);
            Id = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();
        }

        public static Tipos ObterPorId(int id)
        {
            Tipos Tipos = new();
            var cmd = Banco.Abrir();
            cmd.CommandText = $"select * from tipos where id = {id} ";
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Tipos = new Tipos(
                    dr.GetInt32(dr.GetOrdinal("id")),
                    dr.GetString(dr.GetOrdinal("sigla")),
                    dr.GetString(dr.GetOrdinal("rotulo"))
                    );
            }
            return Tipos;
        }

        public static List<Tipos> ObterTodos()
        {
            List<Tipos> Tipos = new();
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from tipos order by rotulo asc";
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Tipos.Add(new Tipos(
                    dr.GetInt32(0),
                    dr.GetString(1),
                    dr.GetString(2)
                    ));
            }
            return Tipos;
        }
        public bool Atualizar()
        {
            bool resposta = false;
            var cmd = Banco.Abrir();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_tipos_update";
            cmd.Parameters.AddWithValue("spsigla", Sigla);
            cmd.Parameters.AddWithValue("sprotulo", Rotulo);
            Id = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();
            if (cmd.ExecuteNonQuery() > 0)
            {
                cmd.Connection.Close();
                resposta = true;
            }
            return resposta;
        }
    }
}