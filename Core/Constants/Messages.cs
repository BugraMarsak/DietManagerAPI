using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Product Added.";
        public static string ProductNameInvalid= "Product name must be more than 2 characters.";
        public static string ProductsListed = "Product listed";
        public static string MaintenanceTime = "Under construction";
        public static string UnitPriceInvalid = "Unvalid unit price.";
        public static string ProductCountOfCategoryError = "There are 10 products in this category.";
        public static string ProductNameAlreadyExists = "Product Name Already Exists";
        public static string CategoryLimitExceded = "Category limit reached";
        public static string AuthorizationDenied = "Authorization Denied";
        public static string UserNotFound = "User Not Found";
        public static string UserRegistered = "User Registered";
        public static string PasswordError = "Password Error";
        public static string SuccessfulLogin = "Successful Login";
        public static string UserAlreadyExists = "User Already Exists";
        public static string AccessTokenCreated = "Access Token Created";

        public static string Listed = "Listelendi.";
        public static string deleted = "Silindi";
        public static string added = "Eklendi";
        public static string changed = "Degisti";
        public static string Wrong = "Bazı şeyler hatalı";
        public static string wrongtime= "ileri bir tarih şeçilmeli!";
    }
}
