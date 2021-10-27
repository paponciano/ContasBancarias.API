using System;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ProjetoVendas
{
    public class Produto
    {
        public int CodProduto { get; set; }

        public string Nome { get; set; }

        public decimal Estoque { get; set; }

        public decimal Valor { get; set; }

        public bool CadastrarProduto(out string mensagemErro)
        {
            mensagemErro = "";
            bool inseriuProduto = false;
            MySqlConnection conn = new("server=127.0.0.1;user=root;password=root;database=bd_vendas");
            try
            {
                conn.Open();
                MySqlCommand cmd = new();
                cmd.Connection = conn;
                cmd.CommandText = "insert into produtos (Nome, Estoque, Valor) values (@nome, @estoque, @valor)";
                cmd.Parameters.AddWithValue("@nome", this.Nome);
                cmd.Parameters.AddWithValue("@estoque", this.Estoque);
                cmd.Parameters.AddWithValue("@valor", this.Valor);
                cmd.ExecuteNonQuery();
                inseriuProduto = true;
            }
            catch(Exception ex)
            {
                mensagemErro = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return inseriuProduto;
        }
        
        public List<Produto> SelecionarProdutos(out string mensagemErro)
        {
            return this.SelecionarProdutos(null, out mensagemErro);
        }
        
        public List<Produto> SelecionarProdutos(int? codProduto, out string mensagemErro)
        {
            mensagemErro = "";
            List<Produto> listaProdutos = new();

            MySqlConnection conn = new("server=123;user=root;password=root;database=bd_vendas");
            try
            {
                conn.Open();
                MySqlCommand cmd = new();
                cmd.Connection = conn;
                
                if (codProduto == null)
                    cmd.CommandText = "select CodProduto, Nome, Estoque, Valor from produtos";
                else
                {
                    cmd.CommandText = "select CodProduto, Nome, Estoque, Valor from produtos where CodProduto = @codproduto";
                    cmd.Parameters.AddWithValue("@codproduto", codProduto);
                }

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Produto produto = new();
                    produto.CodProduto = Convert.ToInt32(dr["CodProduto"]);
                    produto.Nome = Convert.ToString(dr["Nome"]);
                    produto.Estoque = Convert.ToDecimal(dr["Estoque"]);
                    produto.Valor = Convert.ToDecimal(dr["Valor"]);
                    listaProdutos.Add(produto);
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

            return listaProdutos;
        }
    }
}