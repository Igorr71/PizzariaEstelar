using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzariaEstelarClass
{
    public class Clientes
    {
            public int Id { get; set; }
            public string? Nome { get; set; }
            public string? Cpf { get; set; }
            public string Email { get; set; }
            public string? Telefone { get; set; }
            public string Endereco { get; set; }
            public DateTime Data_nascimento { get; set; }

            public Clientes() { }

            public Clientes(string nome, string? cpf, string email, string? telefone, string endereco, DateTime data_nascimento)
            {
                Nome = nome;
                Cpf = cpf;
                Email = email;
                Telefone = telefone;
                Endereco = endereco;
                Data_nascimento = data_nascimento;
            }

            public Clientes(int id, string nome, string? cpf, string email, string? telefone, string endereco, DateTime data_nascimento)
            {
                Id = id;
                Nome = nome;
                Cpf = cpf;
                Email = email;
                Telefone = telefone;
                Endereco = endereco;
                Data_nascimento = data_nascimento;
            }
            public void Inserir()
            {
                var cmd = Banco.Abrir();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_clientes_insert";
                cmd.Parameters.AddWithValue("spnome", Nome);
                cmd.Parameters.AddWithValue("spcpf", Cpf);
                cmd.Parameters.AddWithValue("sptelefone", Telefone);
                cmd.Parameters.AddWithValue("spemail", Email);
                cmd.Parameters.AddWithValue("spdara_nasc", Data_nascimento);
                Id = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Connection.Close();
            }

            public static Clientes ObterPorId(int id)
            {
                Clientes clientes = new();
                var cmd = Banco.Abrir();
                cmd.CommandText = $"select * from clientes where id = {id}";
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    clientes = new(
                        dr.GetInt32(0),
                        dr.GetString(1),
                        dr.GetString(2),
                        dr.GetString(3),
                        dr.GetString(4),
                        dr.GetString(5),
                        dr.GetDateTime(6)
                        );
                }
                return clientes;
            }

            public static List<Clientes> ObterLista()
            {
                List<Clientes> clientes = new();
                var cmd = Banco.Abrir();
                cmd.CommandText = $"select * from clientes order by descricao asc";
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    clientes.Add(new(
                        dr.GetInt32(0),
                        dr.GetString(1),
                        dr.GetString(2),
                        dr.GetString(3),
                        dr.GetString(4),
                        dr.GetString(5),
                        dr.GetDateTime(6)
                        ));

                }
                return clientes;
            }
            public bool Atualizar()
            {
                bool resposta = false;
                var cmd = Banco.Abrir();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_clientes_update";
                cmd.Parameters.AddWithValue("spid", Id);
                cmd.Parameters.AddWithValue("spnome", Nome);
                cmd.Parameters.AddWithValue("sprmail", Email);
                cmd.Parameters.AddWithValue("sptelefone", Telefone);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    cmd.Connection.Close();
                    resposta = true;
                }
                return resposta;
            }
        }
    }