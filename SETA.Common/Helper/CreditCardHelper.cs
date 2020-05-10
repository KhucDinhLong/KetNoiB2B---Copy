using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using SecureNetRestApiSDK.Api.Controllers;
using SecureNetRestApiSDK.Api.Models;
using SecureNetRestApiSDK.Api.Requests;
using SecureNetRestApiSDK.Api.Responses;
using SNET.Core;

namespace SETA.Common.Helper
{
    public class CreditCardHelper
    {


        public static List<CreditCard> Cards = new List<CreditCard>
        {
            new CreditCard
            {
                Type = "maestro",
                Pattern = "^(5(018\\d{8}|018\\d{9}|018\\d{10}|018\\d{11}|018\\d{12}|018\\d{13}|018\\d{14}|018\\d{15}|0[23]\\d{9}|0[23]\\d{10}|0[23]\\d{11}|0[23]\\d{12}|0[23]\\d{13}|0[23]\\d{14}|0[23]\\d{15}|0[23]\\d{16}|[68]\\d{10}|[68]\\d{11}|[68]\\d{12}|[68]\\d{13}|[68]\\d{14}|[68]\\d{15}|[68]\\d{16}|[68]\\d{17})|6(39\\d{9}|39\\d{10}|39\\d{11}|39\\d{12}|39\\d{13}|39\\d{14}|39\\d{15}|39\\d{16}|7\\d{10}|7\\d{11}|7\\d{12}|7\\d{13}|7\\d{14}|7\\d{15}|7\\d{16}|7\\d{17}))",
                Length = new[] { 12, 13, 14, 15, 16, 17, 18, 19 },
                CvcLength = new[] { 3 },
                Luhn = true
            },
            new CreditCard
            {
                Type = "forbrugsforeningen",
                Pattern = "^600\\d{13}$",
                Length = new[] { 16 },
                CvcLength = new[] { 3 },
                Luhn = true
            },
            new CreditCard
            {
                Type = "dankort",
                Pattern = "^5019\\d{12}$",
                Length = new[] { 16 },
                CvcLength = new[] { 3 },
                Luhn = true
            },
            new CreditCard
            {
               Type = "visa",
                Pattern = "^4\\d{12}|4\\d{15}$",
                Length = new[] { 13, 16 },
                CvcLength = new[] { 3 },
                Luhn = true
            },
            new CreditCard
            {
                Type = "mastercard",
                Pattern = "^(5[0-5]|2[2-7])\\d{14}$",
                Length = new[] { 16 },
                CvcLength = new[] { 3 },
                Luhn = true
            },
            new CreditCard
            {
                Type = "amex",
                Pattern = "^3[47]\\d{13}$",
                Length = new[] { 15 },
                CvcLength = new[] { 3, 4 },
                Luhn = true
            },
            new CreditCard
            {
                Type = "dinersclub",
                Pattern = "^3[0689]\\d{12}",
                Length = new[] { 14 },
                CvcLength = new[] { 3 },
                Luhn = true
            },
            new CreditCard
            {
                Type = "discover",
                Pattern = "^6([045]\\d{14}|22\\d{13})$",
                Length = new[] { 16 },
                CvcLength = new[] { 3 },
                Luhn = true
            },
            new CreditCard
            {
                Type = "unionpay",
                Pattern = "^(62|88)\\d{14}$",
                Length = new[] { 16, 17, 18, 19 },
                CvcLength = new[] { 3 },
                Luhn = true
            },
            new CreditCard
            {
                Type = "jcb",
                Pattern = "^35\\d{14}$",
                Length = new[] { 16 },
                CvcLength = new[] { 3 },
                Luhn = true
            },
        };

        public CreditCard ValiCreditCard(string numberCard)
        {
            CreditCard card = new CreditCard();



            return card;
        }

        public static string CreditCardType(string number)
        {
            foreach (var card in CreditCardHelper.Cards)
            {
                if (Regex.IsMatch(number, card.Pattern) && card.Length.Contains(number.Length))
                {
                    return card.Type;
                }
            }
            return "";
        }

        public static string FormatCardNumber(string cardNumber)
        {
            try
            {
                var length = cardNumber.Length;
                var showCard = cardNumber.Substring(cardNumber.Length - 4);
                var prefix = "";
                //fill xxx
                for (var i = 0; i < length - 4; i++)
                {
                    prefix = prefix + "X";
                }
                return prefix + " " + showCard;
            }
            catch(Exception ex)
            {
                return cardNumber;
            }
            
        }
    }
    public class CreditCard
    {
        public string Type { get; set; }
        public string Pattern { get; set; }
        public int[] Length { get; set; }
        public int[] CvcLength { get; set; }
        public bool Luhn { get; set; }
    }
}
