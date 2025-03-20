using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzariaEstelarClass
{
    public class Produtos
    {
        public int Id { get; set; }
        public int Tipo_id { get; set; }
        public string? Descricao { get; set; }
        public double Valor_Unit { get; set; }
        public string? Resumo { get; set; }
        public string? Imagem { get; set; }
        public bool? Destaque { get; set; }

        public Produtos() { }

        public Produtos(int tipo_id, string? descricao, double valorUnit, string? resumo, string imagem, bool destaque)
        {
            Tipo_id = tipo_id;
            Descricao = descricao;
            Valor_Unit = valorUnit;
            Resumo = resumo;
            Imagem = imagem;
            Destaque = destaque;
        }

        public Produtos(int id, int tipo_id, string? descricao, double valorUnit, string? resumo, string imagem, bool destaque)
        {
            Id = id;
            Tipo_id = tipo_id;
            Descricao = descricao;
            Valor_Unit = valorUnit;
            Resumo = resumo;
            Imagem = imagem;
            Destaque = destaque;
        }

        public void Inserir()
        {
            var cmd = Banco.Abrir();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_produtos_insert";
            cmd.Parameters.AddWithValue("sptipo_id", Tipo_id);
            cmd.Parameters.AddWithValue("spvalor_unit", Descricao);
            cmd.Parameters.AddWithValue("spresumo", Resumo);
            cmd.Parameters.AddWithValue("spimagem", Imagem);
            cmd.Parameters.AddWithValue("spdestaque", Destaque);
            Id = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();
        }

        public static Produtos ObterPorId(int id)
        {
            Produtos produtos = new();
            var cmd = Banco.Abrir();
            cmd.CommandText = $"select * from produtos where id = {id}";
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                produtos = new(
                    dr.GetInt32(0),
                    dr.GetInt32(1),
                    dr.GetString(2),
                    dr.GetDouble(3),
                    dr.GetString(4),
                    dr.GetString(5),
                    dr.GetBoolean(6)
                    );

            }
            return produtos;
        }
        public static List<Produtos> ObterLista()
        {
            List<Produtos> produtos = new();
            var cmd = Banco.Abrir();
            cmd.CommandText = $"select * from produtos order by descricao asc";
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                produtos.Add(new(
                    dr.GetInt32(0),
                    dr.GetInt32(1),
                    dr.GetString(2),
                    dr.GetDouble(3),
                    dr.GetString(4),
                    dr.GetString(5),
                    dr.GetBoolean(6)
                    ));
            }
            return produtos;


        }
    }
}