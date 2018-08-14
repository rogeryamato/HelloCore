using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebExtension;

namespace HelloCore.API
{
    /// <summary>
    /// for vue server site
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private static List<BillRecord> Records = new List<BillRecord>()
        {
             new BillRecord (){ BillTypeId=1, CreationTime=DateTime.Now, Id=1, Money =1500, Name="pp"},
             new BillRecord (){ BillTypeId=1, CreationTime=DateTime.Now.AddDays(-1), Id=2, Money =1200, Name="pp"},
             new BillRecord (){ BillTypeId=2, CreationTime=DateTime.Now, Id=3, Money =300, Name="pp2"},
             new BillRecord (){ BillTypeId=3, CreationTime=DateTime.Now, Id=4, Money =1000, Name="pp"},
             new BillRecord (){ BillTypeId=4, CreationTime=DateTime.Now, Id=5, Money =1700, Name="pp"},
             new BillRecord (){ BillTypeId=5, CreationTime=DateTime.Now, Id=6, Money =1880, Name="pp"}
        };
        private static List<BillType> BillTypes = new List<BillType>()
        {
                new BillType(){ Id=1,Name="cloth"},
                new BillType () { Id =2,Name="food"},
                new BillType(){ Id= 3,Name = "transportation"},
                new BillType (){Id = 4,Name="gift"},
                new BillType (){Id = 5,Name="Pet"},
                new BillType (){Id = 6,Name="Medical"}
        };


        [HttpGet]
        [AllowAnonymous]
        public List<BillType> GetBillType()
        {
            return BillTypes;
        }
        

        [HttpPost]
        public void AddBills([FromBody]AddBillModel model)
        {
            Records.Add(new BillRecord()
            {
                Id = Records.Count + 1,
                BillTypeId = model.BillTypeid,
                Money = model.Money,
                Name = BillTypes.First(s => s.Id == model.BillTypeid).Name,
                CreationTime = DateTime.Now
            });
            Response.StatusCode = 200;
        }

        [HttpPost]
        [AllowAnonymous]
        public void DeleteBill([FromBody]DeleteBillModel model)
        {
            var record = Records.FirstOrDefault(s => s.Id == model.Id);
            Records.Remove(record);
            Response.StatusCode = 200;
        }

        [HttpGet]
        public JsonResult GetTotalCount()
        {
            return new JsonResult(Records.Sum(s => s.Money));
        }

        [HttpPost]
        public BillsData GetBills([FromBody]GetBillModel model)
        {
            var selectedRecords = Records.Where(s => s.CreationTime.ToString("yyyy-MM") == model.date.ToString("yyyy-MM"));
            BillsData data = new BillsData();
            data.TotalCount = selectedRecords.Count();
            data.Items = new List<BillRecord>();
            data.Items = selectedRecords.Skip(model.skip).Take(3)?.ToList();
            return data;
        }

        [HttpPost]
        public List<BillCost> GetCount([FromBody]GetCountModel model)
        {
            List<BillCost> bills = new List<BillCost>();
            foreach (var item in BillTypes)
            {
                var count = Records.Where(s => s.BillTypeId == item.Id && s.CreationTime.Date.ToString("yyyy-MM") == model.date.ToString("yyyy-MM")).Sum(s => s.Money);
                if (count > 0)
                    bills.Add(new BillCost()
                    {
                        Name = item.Name,
                        Value = count
                    });
            }
            return bills;
        }
    }

    public class BillType
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }

    public class BillRecord
    {
        public int Id { get; set; }

        public int BillTypeId { get; set; }

        public double Money { get; set; }

        public string Name { get; set; }

        public DateTime CreationTime { get; set; }
    }

    public  class BillCost
    {
        public string Name { get; set; }

        public double Value { get; set; }
    }

    public class BillsData
    {
        public double TotalCount { get; set; }
        
        public List<BillRecord> Items { get; set; }
    }

    public class GetBillModel
    {
        public DateTime date { get; set; }
        public int skip { get; set; }
    }

    public class GetCountModel
    {
        public DateTime date { get; set; }

    }

    public class AddBillModel
    {
        public int BillTypeid { get; set; }

        public double Money { get; set; }
    }
    public class DeleteBillModel
    {
        public int Id { get; set; }
    }
}