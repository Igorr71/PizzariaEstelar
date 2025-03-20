using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzariaEstelarClass
{
    public class Enderecos
    {
        public int Id { get; set; }
        public Clientes Cliente_id { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public string Lougradouro { get; set; }


        public Enderecos() { }

        public Enderecos(int id, Clientes cliente_id, string numero, string complemento, string bairro, string cidade, string estado, string cep, string lougradouro)
        {
            Id = id;
            Cliente_id = cliente_id;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Cep = cep;
            Lougradouro = lougradouro;
        }

        public Enderecos(Clientes cliente_id, string numero, string complemento, string bairro, string cidade, string estado, string cep)
        {
            Cliente_id = cliente_id;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Cep = cep;
        }
        public Enderecos(Clientes cliente_id, string numero, string cidade, string estado, string cep)
        {
            Cliente_id = cliente_id;
            Numero = numero;
            Cidade = cidade;
            Estado = estado;
            Cep = cep;
        }
        public void Inserir()
        {
            var cmd = Banco.Abrir();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_enderecos_insert";
            cmd.Parameters.AddWithValue("spcliente_id", Cliente_id);
            cmd.Parameters.AddWithValue("spcep", Cep);
            cmd.Parameters.AddWithValue("splougradouro", Lougradouro);
            cmd.Parameters.AddWithValue("spnumero", Numero);
            cmd.Parameters.AddWithValue("spcomplemento", Complemento);
            cmd.Parameters.AddWithValue("spbairro", Bairro);
            cmd.Parameters.AddWithValue("spcidade", Cidade);
            Id = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();
        }

        public static Enderecos ObterPorId(int id)
        {
            Enderecos enderecos = new();
            var cmd = Banco.Abrir();
            cmd.CommandText = $"select * from endereco where id = {id}";
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                enderecos = new(
                    dr.GetInt32(0),
           Clientes.ObterPorId(dr.GetInt32(1)),
                    dr.GetString(2),
                    dr.GetString(3),
                    dr.GetString(4),
                    dr.GetString(5),
                    dr.GetString(6),
                  dr.GetString(7),
                 dr.GetString(8)
                    );
            }
            return enderecos;
        }
        public static List<Enderecos> ObterLista()
        {
            List<Enderecos> endereco = new();
            var cmd = Banco.Abrir();
            cmd.CommandText = $"select * from enderecos order by descricao asc";
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                endereco.Add(new(
                    dr.GetInt32(0),
           Clientes.ObterPorId(dr.GetInt32(1)),
                    dr.GetString(2),
                    dr.GetString(3),
                    dr.GetString(4),
                    dr.GetString(5),
                    dr.GetString(6),
                  dr.GetString(7),
                 dr.GetString(8)
                    ));

            }
            return endereco;
        }

        public bool Atualizar()
        {
            bool resposta = false;
            var cmd = Banco.Abrir();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_enderecos_update";
            cmd.Parameters.AddWithValue("spcliente_id", Cliente_id);
            cmd.Parameters.AddWithValue("spcep", Cep);
            cmd.Parameters.AddWithValue("splougradouro", Lougradouro);
            cmd.Parameters.AddWithValue("spnumero", Numero);
            cmd.Parameters.AddWithValue("spcomplemento", Complemento);
            cmd.Parameters.AddWithValue("spbairro", Bairro);
            cmd.Parameters.AddWithValue("spcidade", Cidade);

            if (cmd.ExecuteNonQuery() > 0)
            {
                cmd.Connection.Close();
                resposta = true;
            }
            return resposta;
        }

    }
}


