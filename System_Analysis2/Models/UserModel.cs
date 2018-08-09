using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System_Analysis2.Models
{
    public class UsersModel
    {
        OSSDBEntities db = new OSSDBEntities();
        //AddUsers(Users Users)
        public Users AddUser(Users user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return user;
        }
        //Delete(int ID)
        public Boolean Delete(int ID)
        {
            Users User = db.Users.Find(ID);
            if (User != null)
            {
                db.Users.Remove(User);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        //Edit(Users Users)
        public void Edit(Users Users)
        {
            db.Entry(Users).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        //Login()
        public Users Login(String Email, String PassWord)
        {
            try
            {
                return db.Users.Where(U => U.Email == Email && U.Password == PassWord).Single();
            }
            catch (Exception e)
            {
                return null;
            }
            }
        //ShowProfile()
        public Users ShowProfile(int ID)
        {
            Users User = db.Users.Find(ID);
            return User;
        }
        //ShowUsers()
        public List<Users> ShowUsers()
        {
            List<Users> Users = db.Users.ToList();
            return Users;
        }

        public Users getstaffByID(int id)
        {
            return db.Users.Where(i => i.Id == id).SingleOrDefault();
        }
    }
}