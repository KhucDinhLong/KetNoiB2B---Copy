using System;
using System.Configuration;
using SecureNetRestApiSDK.Api.Controllers;
using SecureNetRestApiSDK.Api.Models;
using SecureNetRestApiSDK.Api.Requests;
using SecureNetRestApiSDK.Api.Responses;
using SETA.Common.Constants;
using SNET.Core;

namespace SETA.Common.Helper
{
    public class PaymentPirceHelper
    {
        public static decimal TeacherPrice(int numberStudents)
        {
            var price = numberStudents * PricePayment.StudentPrice;
            if (price > PricePayment.TeacherMaxPrice)
            {
                price = PricePayment.TeacherMaxPrice;
            }
            return price;
        }

        public static decimal SchoolPrice(int numberStudents)
        {
            var price = numberStudents * PricePayment.StudentPrice;
            if (price > PricePayment.SchoolMaxPrice)
            {
                price = PricePayment.SchoolMaxPrice;
            }
            return price;
        }

        public static decimal AdminPrice(int numberSchools)
        {
            var price = PricePayment.RegisterAdminPrice * numberSchools;
            if (numberSchools >= PricePayment.NumberSchoolMax)
            {
                price = PricePayment.AdminMaxPrice;
            }
            return price;
        }
    }
}

