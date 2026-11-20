using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Buffers.Text;
using System.Text;
using System.Text.Json.Serialization;
using TenatConsume.Models;

namespace TenatConsume.Controllers
{
    public class TenantController : Controller
    {
        public  string baseurl = "https://localhost:44322/api/GreenId/";
        HttpClient _httpClient = new HttpClient();
        public async Task<IActionResult> GetTenant()
        {
            List<Tenant> tenantresult = new List<Tenant>();
            var tresult= await _httpClient.GetAsync(baseurl);
            if (tresult.IsSuccessStatusCode)
            {
              var contentresult=  tresult.Content.ReadAsStringAsync().Result;
                var ten = JsonConvert.DeserializeObject<List<Tenant>>(contentresult);
                tenantresult = ten;



            }
            return View("~/Views/Tenant/Index.cshtml",tenantresult);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Tenant tenantresult = new Tenant();
            var tresult = await _httpClient.GetAsync(baseurl+ "id?id="+id);
            if (tresult.IsSuccessStatusCode)
            {
                var contentresult = tresult.Content.ReadAsStringAsync().Result;
                var ten = JsonConvert.DeserializeObject<Tenant>(contentresult);
                tenantresult = ten;
            }
            return View("~/Views/Tenant/Details.cshtml", tenantresult);
        }

        [HttpGet]
        public async Task<IActionResult> EditTenant(int Tenantid)
        {
            Tenant tenantresult = new Tenant();
            var tresult = await _httpClient.GetAsync(baseurl + "id?id="+ Tenantid);
            if (tresult.IsSuccessStatusCode)
            {
                var contentresult = tresult.Content.ReadAsStringAsync().Result;
                var ten = JsonConvert.DeserializeObject<Tenant>(contentresult);
                tenantresult = ten;
            }
            return View("~/Views/Tenant/Edit.cshtml", tenantresult);
        }

        [HttpPost]
        public async Task<IActionResult> EditTenant(int Tenantid,Tenant request)
        {
            var jsonserialize = JsonConvert.SerializeObject(request);
            StringContent content = new StringContent(jsonserialize, Encoding.UTF8, "application/json");
            var tresult = await _httpClient.PutAsync(baseurl + "id?id=" + Tenantid, content);
            if (tresult.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Records Updated Successfully";
                return RedirectToAction("GetTenant", "Tenant");
               
            }
            return View("~/Views/Tenant/Edit.cshtml", request);
        }

        [HttpGet] 
        public async Task<IActionResult> Create()
        {
            return View("~/Views/Tenant/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tenant request)
        {
     
            if(request != null && ModelState.IsValid)
            {
                
                var jsonserialized= JsonConvert.SerializeObject(request);
                StringContent content = new StringContent(jsonserialized, Encoding.UTF8, "application/json");
                 var msg= await _httpClient.PostAsync(baseurl, content);
                if (msg.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Records Updated Successfully";
                    return RedirectToAction("GetTenant", "Tenant");
                }

            }
            return View("~/Views/Tenant/Create.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Tenantid)
        {
            Tenant tenantresult = new Tenant();
            var tresult = await _httpClient.GetAsync(baseurl + "id?id=" + Tenantid);
            if (tresult.IsSuccessStatusCode)
            {
                var contentresult = tresult.Content.ReadAsStringAsync().Result;
                var ten = JsonConvert.DeserializeObject<Tenant>(contentresult);
                tenantresult = ten;
            }
            return View("~/Views/Tenant/Delete.cshtml", tenantresult);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteTenant(int Tenantid)
        {

            if (Tenantid != null)
            {

                //var jsonserialized = JsonConvert.SerializeObject(request);
                //StringContent content = new StringContent(jsonserialized, Encoding.UTF8, "application/json");
                var msg = await _httpClient.DeleteAsync(baseurl+ "id?id="+ Tenantid);
                if (msg.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Records Delete Successfully";
                    return RedirectToAction("GetTenant", "Tenant");
                }

            }
            return View("~/Views/Tenant/Create.cshtml");
        }


    }
}
