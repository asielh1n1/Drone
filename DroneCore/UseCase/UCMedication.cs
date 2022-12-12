using DroneCore.Entities;
using DroneCore.Interfaces;
using DroneCore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneCore.UseCase
{
    public class UCMedication
    {
        private IUnitOfWork _unitOfWork;

        public UCMedication(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Returns a list of medications
        /// </summary>
        /// <returns></returns>
        public List<Medication> List()
        {
            return _unitOfWork.Medication.Find().ToList();
        }

        /// <summary>
        /// Add a new medication
        /// </summary>
        /// <param name="name">Allowed only letters, numbers, ‘-‘, ‘_’</param>
        /// <param name="weight"></param>
        /// <param name="code">Allowed only upper case letters, underscore and numbers</param>
        /// <param name="image">Picture of the medication case</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Medication Add(string name, int weight, string code, string? image)
        {
            if (string.IsNullOrEmpty(name))
                throw new DroneException("The name cannot be empty or null.");
            if (!Medication.isValidName(name))
                throw new DroneException("Invalid name, only letters, numbers, ‘-‘, ‘_’");
            if (!Medication.isValidName(code))
                throw new DroneException("Invalid name, only letters, numbers, ‘-‘, ‘_’");
            Medication  medication = new Medication(name, weight, code.ToUpper(), image);
            _unitOfWork.Medication.Add(medication);
            _unitOfWork.Save();
            return medication;
        }
    }
}
