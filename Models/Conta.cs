using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Models
{
    public class Conta
    {
        public int Numero { get; set; }

        public int Agencia { get; set; }

        public int Tipo { get; set; }

        public decimal Saldo { get; set; }

        public int CodigoCliente { get; set; }

        public Cliente ClienteTitular { get; set; }

        public List<Conta> SelecionarContas(out string mensagemErro)
        {
            return this.SelecionarContas(null, out mensagemErro);
        }

        public List<Conta> SelecionarContas(int? numero, out string mensagemErro)
        {
            mensagemErro = "";
            List<Conta> listaContas = new();

            MySqlConnection conn = new("server=127.0.0.1;user=root;password=root;database=controlbank");
            try
            {
                conn.Open();
                MySqlCommand cmd = new();
                cmd.Connection = conn;
                if (numero == null)
                    cmd.CommandText = "select numero, agencia, tipo, saldo, codigocliente from conta";
                else
                {
                    cmd.CommandText = "select numero, agencia, tipo, saldo, codigocliente from conta where numero = @numero";
                    cmd.Parameters.AddWithValue("@numero", numero);
                }

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Conta conta = new();
                    conta.Numero = Convert.ToInt32(dr["numero"]);
                    conta.Agencia = Convert.ToInt32(dr["agencia"]);
                    conta.Tipo = Convert.ToInt32(dr["tipo"]);
                    conta.Saldo = Convert.ToDecimal(dr["saldo"]);
                    conta.CodigoCliente = Convert.ToInt32(dr["codigocliente"]);
                    listaContas.Add(conta);
                }
                dr.Close();

                foreach(var conta in listaContas)
                {
                    MySqlCommand cmdCliente = new();
                    cmdCliente.Connection = conn;
                    cmdCliente.CommandText = "select codigo, nome, cpf, telefone, email from cliente where codigo = @codigocliente";
                    cmdCliente.Parameters.AddWithValue("@codigocliente", conta.CodigoCliente);
                    var drCliente = cmdCliente.ExecuteReader();
                    while (drCliente.Read())
                    {
                        conta.ClienteTitular = new Cliente();
                        conta.ClienteTitular.Codigo = Convert.ToInt32(drCliente["codigo"]);
                        conta.ClienteTitular.Nome = Convert.ToString(drCliente["nome"]);
                        conta.ClienteTitular.Cpf = Convert.ToString(drCliente["cpf"]);
                        conta.ClienteTitular.Telefone = Convert.ToString(drCliente["telefone"]);
                        conta.ClienteTitular.Email = Convert.ToString(drCliente["email"]);
                    }
                    drCliente.Close();
                }
            }
            catch(Exception ex)
            {
                mensagemErro = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return listaContas;
        }
    }
}