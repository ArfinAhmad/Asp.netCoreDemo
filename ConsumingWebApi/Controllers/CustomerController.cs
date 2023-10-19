using ConsumingWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;

namespace ConsumingWebApi.Controllers
{
    public class CustomerController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7297/api");
        private readonly HttpClient _client;

        public CustomerController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }



        [HttpGet]
        public IActionResult Index()

        {
            List<CustomerViewModel> customer = new List<CustomerViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Customer/GetCustomers").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                customer = JsonConvert.DeserializeObject<List<CustomerViewModel>>(data);
            }

            return View(customer);
        }





        [HttpGet]

        public IActionResult Insert()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Insert(CustomerViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Customer/AddCustomer", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Customer Created.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
            return View();
        }






        [HttpGet]

        public IActionResult Edit(int id)

        {
            try
            {
                CustomerViewModel customer = new CustomerViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Customer/GetCustomer/" + id).Result;        // it should be changed by get id action method
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    customer = JsonConvert.DeserializeObject<CustomerViewModel>(data);
                }

                return View(customer);
            }

            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                  return View();
            }

        }








        [HttpPost]
        public IActionResult Edit(CustomerViewModel model,int id)
        {
            try
            {
                CustomerViewModel customer = new CustomerViewModel();
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Customer/UpdateCustomer/" + id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Customer Updated.";
                    return RedirectToAction("Index");
                }
                return View(customer);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();

            }
           
        }


       



        [HttpGet] 
        public IActionResult Delete(int id) 
        {
            try
            {
                CustomerViewModel customer = new CustomerViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Customer/GetCustomer/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    customer = JsonConvert.DeserializeObject<CustomerViewModel>(data);
                }
                return View(customer);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
       
        }



        [HttpPost,ActionName("Delete")] 
        public IActionResult DeleteConfirmed(int id) 
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Customer/DeleteCustomer/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Customer Deleted.";
                    return RedirectToAction("Index");
                }
            
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();

            }
            return View();
        }

    } 
}
