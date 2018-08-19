using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ConsoleApp1
{
    class Program
    {
        static int InsertedId;

        static void Main(string[] args)
        {

            using (var db = new ContosoUniversityEntities())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Database.Log = Console.WriteLine;

                        var department = db.GetDepartment();
                        foreach (var item in department)
                            Console.WriteLine(item.Name);
                        //db.SaveChanges();

                        //trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                    }

                }
            }
            //using (var db = new ContosoUniversityEntities())
            //{
            //    db.Database.Log = Console.WriteLine;
            //    var dept = db.Department.Find(57);
            //    Console.WriteLine($"{dept.DepartmentID}\t{dept.Name}");
            //}
            System.Console.Read();
        }

        private static void InsertDepartment(ContosoUniversityEntities db)
        {
            var dept = new Department()
            {
                Name = "Will",
                Budget = 100,
                StartDate = DateTime.Now

            };

            db.Department.Add(dept);
            db.SaveChanges();

            InsertedId = dept.DepartmentID;
        }

        private static void InsertFromSelect(ContosoUniversityEntities db)
        {
            var dept = db.Department.Find(1);
        }
        private static void UpdateDepartment(ContosoUniversityEntities db)
        {
            var dept = db.Department.Find(InsertedId);
            dept.Name = "John";
            db.SaveChanges();
        }

        private static void RemoveDepartmentByState(ContosoUniversityEntities db)
        {
            db.Entry(new Department() { DepartmentID = 56 }).State = EntityState.Deleted;
            db.SaveChanges();

        }
        private static void RemoveDepartment(ContosoUniversityEntities db)
        {
            db.Department.Remove(db.Department.Find(InsertedId));
            db.SaveChanges();
        }

        private static void QueryCourse(ContosoUniversityEntities db)
        {
            var data = from p in db.Course select p;

            foreach (var item in data)
            {
                Console.WriteLine(item.CourseID);
                Console.WriteLine(item.Title);
                Console.WriteLine();
            }
        }
    }
}
