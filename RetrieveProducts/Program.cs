using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;
using System.Web.Script.Serialization;
using System.Net.NetworkInformation;

namespace RetrieveProducts
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int quantity { get; set; }
        public Double sale_amount { get; set; }
    }
    class Program
    {
        static HttpClient client = new HttpClient();

        static void ShowProduct(Product product)
        {
            Console.WriteLine($"Name: {product.Name}\tquantity: " +
                $"{product.quantity}\tsale_amount: {product.sale_amount}");
        }

        //static async Task<Uri> CreateProductAsync(Product product)
        //{

        //    //  HttpResponseMessage response =  client.PostAsJsonAsync("products/1",product).Result;

        //    //var response = client.PostAsync("products", new StringContent(new JavaScriptSerializer().Serialize(product), Encoding.UTF8, "application/json")).Result;
        //    HttpResponseMessage response = await client.PostAsJsonAsync("api/products", product);
        //    //await client.PostAsJsonAsync(
        //    //"api/products/1", product);
        //    response.EnsureSuccessStatusCode();

        //    // return URI of the created resource.
        //    return response.Headers.Location;
        //}
        static async Task<Uri> CreateProductAsync(Product product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/products", product);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<Product> GetProductAsync(string path)
        {
            Product product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
        }

        static async Task<Product> UpdateProductAsync(Product product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/products/{product.Id}", product);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            product = await response.Content.ReadAsAsync<Product>();
            return product;
        }

        static async Task<HttpStatusCode> DeleteProductAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/products/{id}");
            return response.StatusCode;
        }
        static void Main()
        {
        }
        //static void Main()
        //{
        //    HttpListener httpListner = new HttpListener();
        //    httpListner.Prefixes.Add("http://localhost:8080/");
        //    httpListner.Start();
        //    Console.WriteLine("Port: 8080 status: " + (PortInUse(8080) ? "in use" : "not in use"));


        //    Console.ReadKey();


        //    httpListner.Close();
        //    RunAsync().GetAwaiter().GetResult();
        //}
        public static bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();


            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }


            return inUse;
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:8080/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new product
                Product product = new Product
                {
                    Name = "Tea",
                    quantity = 100,
                    sale_amount =300
                };

                var url = CreateProductAsync(product);
                //await CreateProductAsync(product);
                Console.WriteLine($"Created at {url}");

                // Get the product
                //product = await GetProductAsync(url.PathAndQuery);
                //ShowProduct(product);

                // Update the product
                Console.WriteLine("Updating price...");
                product.sale_amount = 80;
                await UpdateProductAsync(product);

                // Get the updated product
                //product = await GetProductAsync(url.PathAndQuery);
                //ShowProduct(product);

                // Delete the product
                var statusCode = await DeleteProductAsync(product.Id.ToString());
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}



