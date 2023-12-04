using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace petsSharp.Pages
{
    public class addProdutoModel : PageModel
    {
        [BindProperty]
        public string Nome { get; set; }
        [BindProperty]
        public string Categoria { get; set; }
        [BindProperty]
        public string Descricao { get; set; }
        [BindProperty]
        public string Valor { get; set; }
        [BindProperty]
        public string Fornecedor { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            BancoDeDados bancoDeDados = new BancoDeDados();
            SqlDataReader leitor;
            int codigo_fornecedor;
            int codigo_produto;

            bancoDeDados.abrirConexao();

            leitor = bancoDeDados.executarQuery("select count(distinct codigo_produto) from tb_produto");
            if (leitor.HasRows)
            {
                leitor.Read();
                codigo_produto = leitor.GetInt32(0) + 1;
            }
            else
            {
                codigo_produto = 1;
            }

            bancoDeDados.fechar();

            string query = $"select codigo_fornecedor from tb_fornecedor where nome = '{Fornecedor.ToLower()}'";
            bancoDeDados.abrirConexao();
            leitor = bancoDeDados.executarQuery(query);
            if (leitor.HasRows)
            {
                leitor.Read();
                codigo_fornecedor = leitor.GetInt32(0);
            }
            else
            {
                codigo_fornecedor = -1;
            }
            bancoDeDados.fechar();

            if(codigo_fornecedor == -1)
            {
                Fornecedor = "ERRO: N�O ENCONTRADO";
                return Page();
            }
            query = $"insert into tb_produto (codigo_produto, nome, categoria, descricao, valor, codigo_fornecedor) values " +
                $"({codigo_produto}, '{Nome}', '{Categoria}', '{Descricao}', '{Valor}', '{codigo_fornecedor}')";

            bancoDeDados.manipularDado(query);
            return RedirectToPage("/dashboard");
        }
    }
}
