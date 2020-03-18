using EFProfiler.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EFProfiler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="str"></param>
        [HttpPut]
        public void Put(string str)
        {
            using (EFContext context = new EFContext())
            {
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();
                var list = new List<Student>();
                for (int i = 0; i < 10; i++)
                {
                    list.Add(new Student()
                    {
                        Name = str
                    });
                }
                context.Student.AddRange(list);
                context.SaveChanges();
            }
        }
    }
}