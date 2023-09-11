using DroneTest.Infrastructure;
using DroneCore.UseCase;
using DroneCore.Entities;
using DroneCore.Util;

namespace MedicationTest.Test
{
    public class MedicationTest
    {
        [Fact]
        public void Add()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                MedicationUC MedicationUC = new MedicationUC(_unitOfWork);
                Medication medication = MedicationUC.Add("Simvastatina", 100, "sim", "");
                Assert.NotNull(medication);
            }
        }

        [Theory]
        //Empty name
        [InlineData("", 100, "sim","")]
        //Invalid name
        [InlineData("sdd@$", 100, "sim", "")]
        public void InvalidAdd(string name, int weight, string code, string? image)
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                MedicationUC MedicationUC = new MedicationUC(_unitOfWork);
                Assert.Throws<DroneException>(() => MedicationUC.Add(name, weight, code, image));
            }

        }

        [Fact]
        public void List()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                MedicationUC MedicationUC = new MedicationUC(_unitOfWork);
                MedicationUC.Add("Simvastatina", 100, "sim", "");
                MedicationUC.Add("Aspirina", 100, "asp", "");
                MedicationUC.Add("Omeprazol", 100, "ome", "");
                List<Medication> list = MedicationUC.List();
                Assert.Equal(3, list.Count);
            }
        }


    }

    
}