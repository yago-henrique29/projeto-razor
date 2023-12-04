using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace petsSharp.Pages
{
    public class dashboardModel : PageModel
    {
        [BindProperty]
        //public string nome { get; set; }
        public SqlDataReader leitor { get; set; }
        public int totalVenda { get; set; }
        public int totalProdutosCadastrados { get; set; }

        public int totalServicos { get; set; }
        public int totalServicosPendentes { get; set; }
        public int totalFornecedores { get; set; }
        public decimal valorTotal { get; set; }

        public dashboardModel()
        {
            BancoDeDados bancoDeDados = new BancoDeDados();

            bancoDeDados.abrirConexao();

            leitor = bancoDeDados.executarQuery("select count(distinct codigo_venda) from tb_venda");
            if (leitor.HasRows)
            {
                leitor.Read();
                totalVenda = leitor.GetInt32(0);
            }
            else
            {
                totalVenda = 0;
            }

            bancoDeDados.fechar();
            bancoDeDados.abrirConexao();

            leitor = bancoDeDados.executarQuery("select count(distinct codigo_produto) from tb_produto");

            if (leitor.HasRows)
            {
                leitor.Read();
                totalProdutosCadastrados = leitor.GetInt32(0);
            }
            else
            {
                totalProdutosCadastrados = 0;
            }

            bancoDeDados.fechar();
            bancoDeDados.abrirConexao();

            leitor = bancoDeDados.executarQuery("select count(distinct codigo_servico) from tb_servicos");

            if (leitor.HasRows)
            {
                leitor.Read();
                totalServicos = leitor.GetInt32(0);
            }
            else
            {
                totalServicos = 0;
            }

            bancoDeDados.fechar();
            bancoDeDados.abrirConexao();

            leitor = bancoDeDados.executarQuery("select count( servico_realizado) from tb_servicos where servico_realizado = 'nao'");

            if (leitor.HasRows)
            {
                leitor.Read();
                totalServicosPendentes = leitor.GetInt32(0);
            }
            else
            {
                totalServicosPendentes = 0;
            }

            bancoDeDados.fechar();

            bancoDeDados.abrirConexao();

            leitor = bancoDeDados.executarQuery("select count(distinct codigo_fornecedor) from tb_fornecedor");

            if (leitor.HasRows)
            {
                leitor.Read();
                totalFornecedores = leitor.GetInt32(0);
            }
            else
            {
                totalFornecedores = 0;
            }

            bancoDeDados.fechar();

            bancoDeDados.abrirConexao();

            leitor = bancoDeDados.executarQuery("select sum(valor) from tb_produto");

            if (leitor.HasRows)
            {
                leitor.Read();
                valorTotal = leitor.GetDecimal(0);
            }
            else
            {
                valorTotal = 0;
            }

            bancoDeDados.fechar();
        }

        public void OnGet()
        {
            //BancoDeDados bancoDeDados = new BancoDeDados();
            //bancoDeDados.manipularDado("insert into tb_cliente (nome, codigo_cliente) values ('yago', 1)");
            //Console.WriteLine(bancoDeDados.executarQuery("select * from tb_cliente"));
        }
        public void OnPost() { }

        public void OnPostTeste()
        {
            
        }
    }
}
