using System;
using System.Collections.Generic;
using MySqlConnector;

namespace ExemploCRUD.Models
{
    public class UsuarioBanco
    {
        private const string DadosConexao = "Database=exemplo_crud; Data Source=localhost; User Id=root;";


        public Usuario ValidarLogin(Usuario usuario)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string Query = "SELECT * FROM Usuario WHERE Login=@Login AND Senha=@Senha";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);
            Comando.Parameters.AddWithValue("@Login", usuario.Login);
            Comando.Parameters.AddWithValue("@Senha", usuario.Senha);
            MySqlDataReader Reader = Comando.ExecuteReader();

            Usuario UsuarioEncontrado = new Usuario();

            if(Reader.Read())
            {
                UsuarioEncontrado.Id = Reader.GetInt32("Id");
                
                if(!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                    UsuarioEncontrado.Nome = Reader.GetString("Nome");
                if(!Reader.IsDBNull(Reader.GetOrdinal("Login")))                    
                    UsuarioEncontrado.Login = Reader.GetString("Login");
                if(!Reader.IsDBNull(Reader.GetOrdinal("Senha")))                    
                    UsuarioEncontrado.Senha = Reader.GetString("Senha");
            }
            Conexao.Close();
            return UsuarioEncontrado;
        }

        public List<Usuario> Listar()
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string Query = "SELECT * FROM Usuario";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);
            MySqlDataReader Reader = Comando.ExecuteReader();

            List<Usuario> Lista = new List<Usuario>();

            while (Reader.Read())
            {
                Usuario UsuarioEncontrado = new Usuario();
                UsuarioEncontrado.Id = Reader.GetInt32("Id");
                
                if(!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                    UsuarioEncontrado.Nome = Reader.GetString("Nome");
                if(!Reader.IsDBNull(Reader.GetOrdinal("Login")))                    
                    UsuarioEncontrado.Login = Reader.GetString("Login");
                if(!Reader.IsDBNull(Reader.GetOrdinal("Senha")))                    
                    UsuarioEncontrado.Senha = Reader.GetString("Senha");

                Lista.Add(UsuarioEncontrado);
            }
            Conexao.Close();
            return Lista;            
        }

        public void Inserir(Usuario usuario)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string Query = "INSERT INTO Usuario (Nome, Login, Senha) VALUES (@Nome, @Login, @Senha)";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);
            Comando.Parameters.AddWithValue("@Nome", usuario.Nome);
            Comando.Parameters.AddWithValue("@Login", usuario.Login);
            Comando.Parameters.AddWithValue("@Senha", usuario.Senha);
            Comando.ExecuteNonQuery();
            Conexao.Close();
        }

        public void Atualizar(Usuario usuario)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string Query = "UPDATE Usuario SET Nome=@Nome, Login=@Login, Senha=@Senha WHERE Id=@Id";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);
            Comando.Parameters.AddWithValue("@Nome", usuario.Nome);
            Comando.Parameters.AddWithValue("@Login", usuario.Login);
            Comando.Parameters.AddWithValue("@Senha", usuario.Senha);
            Comando.Parameters.AddWithValue("@Id", usuario.Id);
            Comando.ExecuteNonQuery();
            Conexao.Close();            
        }

        public void Remover(int Id)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string Query = "DELETE FROM Usuario WHERE Id=@Id";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);
            Comando.Parameters.AddWithValue("@Id", Id);
            Comando.ExecuteNonQuery();
            Conexao.Close();  
        }

        public Usuario BuscarPorId(int Id)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string Query = "SELECT * FROM Usuario WHERE Id=@Id";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);
            Comando.Parameters.AddWithValue("@Id", Id);
            MySqlDataReader Reader = Comando.ExecuteReader();

            Usuario UsuarioEncontrado = new Usuario();

            if(Reader.Read())
            {
                UsuarioEncontrado.Id = Reader.GetInt32("Id");
                
                if(!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                    UsuarioEncontrado.Nome = Reader.GetString("Nome");
                if(!Reader.IsDBNull(Reader.GetOrdinal("Login")))                    
                    UsuarioEncontrado.Login = Reader.GetString("Login");
                if(!Reader.IsDBNull(Reader.GetOrdinal("Senha")))                    
                    UsuarioEncontrado.Senha = Reader.GetString("Senha");
            }
            Conexao.Close();
            return UsuarioEncontrado;            
        }

        public void TestarConexao()
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            Console.WriteLine("Banco de dados funcionando!");
            Conexao.Close();
        }
    }
}