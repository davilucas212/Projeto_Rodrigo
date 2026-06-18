using Projeto_Rodrigo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Projeto_Rodrigo.Services
{
    public class SalaService
    {
        private string connectionString =
            "Server=(localdb)\\mssqllocaldb;" +
            "Database=reunioes;" +
            "Trusted_Connection=true";

        public bool Criar(Sala sala, out List<ValidationResult> erros)
        {
            erros = new List<ValidationResult>();

            if (!Validar(sala, out erros))
            {
                return false;
            }

            var comando =
                @"INSERT INTO salas(nome, andar, quantidadeassentos)
                  VALUES(@Nome, @Andar, @QuantidadeAssentos)";

            var conexao = new SqlConnection(connectionString);

            conexao.Open();

            var sqlCommand = new SqlCommand(comando, conexao);

            sqlCommand.Parameters.Add("@Nome", SqlDbType.Text).Value = sala.Nome;

            sqlCommand.Parameters.Add("@Andar", SqlDbType.Int).Value = sala.Andar;

            sqlCommand.Parameters.Add("@QuantidadeAssentos", SqlDbType.Int)
                .Value = sala.QuantidadeAssentos;

            sqlCommand.ExecuteNonQuery();

            conexao.Close();

            return true;
        }

        public bool Validar(Sala sala, out List<ValidationResult> erros)
        {
            var contexto = new ValidationContext(sala);

            erros = new List<ValidationResult>();

            return Validator.TryValidateObject(
                sala,
                contexto,
                erros,
                true
            );
        }

        public List<Sala> Listar(int numeroPagina)
        {
            var lista = new List<Sala>();

            int itensPorPagina = 10;
            int pularItens = (numeroPagina - 1) * itensPorPagina;

            var comando =
                @"SELECT id, nome, andar, quantidadeassentos
          FROM salas
          ORDER BY andar ASC, nome ASC
          OFFSET @PularItens ROWS
          FETCH NEXT @ItensPorPagina ROWS ONLY";

            using (var conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                var sqlCommand = new SqlCommand(comando, conexao);
                sqlCommand.Parameters.Add("@PularItens", SqlDbType.Int).Value = pularItens;
                sqlCommand.Parameters.Add("@ItensPorPagina", SqlDbType.Int).Value = itensPorPagina;

                using (var leitor = sqlCommand.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        var sala = new Sala
                        {
                            Id = leitor.GetInt32(0),
                            Nome = leitor.GetString(1),
                            Andar = leitor.GetInt32(2),
                            QuantidadeAssentos = leitor.GetInt32(3)
                        };

                        lista.Add(sala);
                    }
                }
            }

            return lista;
        }
        public Sala BuscarPorId(int id)
        {
            Sala sala = null;

            var comando = @"
        SELECT id, nome, andar, quantidadeassentos
        FROM salas
        WHERE id = @Id";

            using var conexao = new SqlConnection(connectionString);

            conexao.Open();

            using var sqlCommand = new SqlCommand(comando, conexao);

            sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            using var leitor = sqlCommand.ExecuteReader();

            if (leitor.Read())
            {
                sala = new Sala
                {
                    Id = leitor.GetInt32(0),
                    Nome = leitor.GetString(1),
                    Andar = leitor.GetInt32(2),
                    QuantidadeAssentos = leitor.GetInt32(3)
                };
            }

            return sala;
        }


        public List<Sala> Buscar(string nome)
        {
            var salas = new List<Sala>();

            var comando = @"
        SELECT id, nome, andar, quantidadeassentos
        FROM salas
        WHERE nome LIKE @Nome";

            using var conexao = new SqlConnection(connectionString);

            conexao.Open();

            using var sqlCommand = new SqlCommand(comando, conexao);

            sqlCommand.Parameters.Add("@Nome", SqlDbType.VarChar).Value = $"%{nome}%";

            using var leitor = sqlCommand.ExecuteReader();

            while (leitor.Read())
            {
                salas.Add(new Sala
                {
                    Id = leitor.GetInt32(0),
                    Nome = leitor.GetString(1),
                    Andar = leitor.GetInt32(2),
                    QuantidadeAssentos = leitor.GetInt32(3)
                });
            }

            return salas;
        }


        public bool Atualizar(Sala sala, out List<ValidationResult> erros)
        {
            erros = new List<ValidationResult>();

            if (!Validar(sala, out erros))
            {
                return false;
            }

            var comando =
                @"UPDATE salas
          SET nome = @Nome,
              andar = @Andar,
              quantidadeassentos = @QuantidadeAssentos
          WHERE id = @Id";

            var conexao = new SqlConnection(connectionString);

            conexao.Open();

            var sqlCommand = new SqlCommand(comando, conexao);

            sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = sala.Id;
            sqlCommand.Parameters.Add("@Nome", SqlDbType.Text).Value = sala.Nome;
            sqlCommand.Parameters.Add("@Andar", SqlDbType.Int).Value = sala.Andar;
            sqlCommand.Parameters.Add("@QuantidadeAssentos", SqlDbType.Int)
                .Value = sala.QuantidadeAssentos;

            sqlCommand.ExecuteNonQuery();

            conexao.Close();

            return true;
        }

        public void Excluir(int id)
        {
            var comando = "DELETE FROM salas WHERE id = @Id";

            var conexao = new SqlConnection(connectionString);

            conexao.Open();

            var sqlCommand = new SqlCommand(comando, conexao);

            sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            sqlCommand.ExecuteNonQuery();

            conexao.Close();
        }
    }
}

