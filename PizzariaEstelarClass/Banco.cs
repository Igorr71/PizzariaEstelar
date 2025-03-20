using MySql.Data.MySqlClient;
namespace PizzariaEstelarClass
{
    public static class Banco // static pois não precisaremos criar uma instancia de Banco para conectar às nossas base dados
    {
        public static MySqlCommand Abrir(int cod = 0) // método para abrir conexão
        {
            string strcon = @"server=localhost;database=pizzaria_estelar;user=root;password=''";
            MySqlConnection cn = new(strcon);
            MySqlCommand cmd = new();
            try
            {
                cn.Open();
                cmd.Connection = cn;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return cmd;


        }
    }
}
