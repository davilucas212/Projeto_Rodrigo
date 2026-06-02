using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto_Rodrigo.Services
{
    using global::Projeto_Rodrigo.Models;
  
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Data.SqlClient;

    namespace Projeto_Rodrigo.Services
    {
        public class ReservaService
        {
            private string connectionString =
                "Server=(localdb)\\mssqllocaldb;" +
                "Database=reunioes;" +
                "Trusted_Connection=true";

            public bool Criar(Reserva reserva, out List<ValidationResult> erros)
            {
                erros = new List<ValidationResult>();

                if (!Validar(reserva, out erros))
                {
                    return false;
                }

                var comando =
                    @"INSERT INTO reservas(inicio, fim, salaid)
                  VALUES(@Inicio, @Fim, @SalaId)";

                var conexao = new SqlConnection(connectionString);

                conexao.Open();

                var sqlCommand = new SqlCommand(comando, conexao);

                sqlCommand.Parameters.Add("@Inicio", SqlDbType.DateTime)
                    .Value = reserva.Inicio;

                sqlCommand.Parameters.Add("@Fim", SqlDbType.DateTime)
                    .Value = reserva.Fim;

                sqlCommand.Parameters.Add("@SalaId", SqlDbType.Int)
                    .Value = reserva.SalaId;

                sqlCommand.ExecuteNonQuery();

                conexao.Close();

                return true;
            }

            public bool Validar(Reserva reserva, out List<ValidationResult> erros)
            {
                var contexto = new ValidationContext(reserva);

                erros = new List<ValidationResult>();

                return Validator.TryValidateObject(
                    reserva,
                    contexto,
                    erros,
                    true
                );
            }

            public List<Reserva> Listar(int numeroPagina)
            {
                var lista = new List<Reserva>();

                int itensPorPagina = 10;
                int pularItens = (numeroPagina - 1) * itensPorPagina;

                var comando =
                    @"SELECT id, inicio, fim, salaid
                  FROM reservas
                  ORDER BY inicio";

                var conexao = new SqlConnection(connectionString);

                conexao.Open();

                var sqlCommand = new SqlCommand(comando, conexao);

                var leitor = sqlCommand.ExecuteReader();

                while (leitor.Read())
                {
                    var reserva = new Reserva
                    {
                        Id = leitor.GetInt32(0),
                        Inicio = leitor.GetDateTime(1),
                        Fim = leitor.GetDateTime(2),
                        SalaId = leitor.GetInt32(3)
                    };

                    lista.Add(reserva);
                }

                conexao.Close();

                return lista;
            }

            public Reserva Buscar(int id)
            {
                Reserva reserva = null;

                var comando =
                    @"SELECT id, inicio, fim, salaid
                  FROM reservas
                  WHERE id = @Id";

                var conexao = new SqlConnection(connectionString);

                conexao.Open();

                var sqlCommand = new SqlCommand(comando, conexao);

                sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                var leitor = sqlCommand.ExecuteReader();

                if (leitor.Read())
                {
                    reserva = new Reserva
                    {
                        Id = leitor.GetInt32(0),
                        Inicio = leitor.GetDateTime(1),
                        Fim = leitor.GetDateTime(2),
                        SalaId = leitor.GetInt32(3)
                    };
                }

                conexao.Close();

                return reserva;
            }

            public void Excluir(int id)
            {
                var comando = "DELETE FROM reservas WHERE id = @Id";

                var conexao = new SqlConnection(connectionString);

                conexao.Open();

                var sqlCommand = new SqlCommand(comando, conexao);

                sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                sqlCommand.ExecuteNonQuery();

                conexao.Close();
            }
        }
    }
}
