﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DroneCore.Entities
{
    public class Medication
    {
        public Medication()
        {
        }

        public Medication(string name, int weight, string code, string? image)
        {
            Name = name;
            Weight = weight;
            Code = code;
            Image = image;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public string Code { get; set; }
        public string? Image { get; set; }

        /// <summary>
        /// Validate the name of the drug
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool isValidName(string name)
        {
            Regex Validator = new Regex(@"^[- _ a-zA-Z0-9]*$");
            return Validator.IsMatch(name);
        }
    }
}
