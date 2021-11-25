using Web.Data.Models;

namespace Web.Data.Interfaces
{
    public interface IAllCars
    {
        IEnumerable<Car> Cars { get; }
        IEnumerable<Car> Cars2 { get; }

        Car GetObjectCar(int carId);
    }
}
