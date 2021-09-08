using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert
{
    class Monitoramento
    {
        public void Pesquisar()
        {
            //Abrindo uma instancia do navegador
            IWebDriver driver = new ChromeDriver();

            //Navegando no Google
            driver.Navigate().GoToUrl("https://www.infomoney.com.br/cotacoes/petrobras-petr4/");

            //Mapeando a Tag onde esta´o valor da PETR4
            var title = driver.FindElements(By.XPath("/html/body/div[4]/div/div[1]/div[1]/div/div[3]/div[1]/p"));

            //Mostrando o valor da PETR4 no console
            foreach (var item in title)
            {
                Console.WriteLine(item.Text);

                //ENVIANDO EMAIL COM SMTP
                SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

                client.Port = 587;

                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.UseDefaultCredentials = false;

                //adcionar email e senha nos dois parametros exigidos
                System.Net.NetworkCredential credential = new System.Net.NetworkCredential("EMAIL_REMETENTE@hotmail.com", "SENHA_DO_EMAIL_REMETENTE");

                client.EnableSsl = true;

                client.Credentials = credential;

                //chamando email que está na página de configuração
                string v1 = System.Configuration.ConfigurationManager.AppSettings["e1"];

                MailMessage message = new MailMessage("EMAIL_REMETENTE@hotmail.com", v1);

                message.Subject = "valor da cotação PETR4";

                Double v;
                v = Convert.ToDouble(item.Text);
                Console.WriteLine(v);
                if (v >= 22.69)
                {
                    message.Body = $"<h3>{v}</h3><h1>HORA DE VERDER</h1>";
                }
                else if (v <= 22.59)
                {
                    message.Body = $"<h3>{v}</h3><h1>HORA DE COMPRAR</h1>";
                }
                else
                {
                    message.Body = $"<h3>{v}</h3><h1>PODE FICAR TRANQUILO</h1>";
                }

                message.IsBodyHtml = true;
                client.Send(message);
            }

            //Fechar pagina do google
            driver.Quit();
        }
    }
}
