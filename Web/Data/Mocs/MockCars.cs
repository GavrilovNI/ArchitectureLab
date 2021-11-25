using Web.Data.Interfaces;
using Web.Data.Models;

namespace Web.Data.Mocs
{
    public class MockCars : IAllCars
    {
        private readonly ICarsCategory carsCategory = new MocksCategory();

        public IEnumerable<Car> Cars
        {
            get
            {
                return new List<Car>()
                {
                    new Car() { Name = "Car1", Category = carsCategory.AllCategories.First() },
                    new Car() { Name = "Car2", Category = carsCategory.AllCategories.First() },
                    new Car() { Name = "Car3", Category = carsCategory.AllCategories.Last() },
                };
            }
        }
        public IEnumerable<Car> Cars2 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Car GetObjectCar(int carId)
        {
            throw new NotImplementedException();
        }
    }
}
