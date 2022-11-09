using ProdtestApi.Models;

namespace ProdtestApi.Services;

public interface IKitchenService
{
    OrderHandled handleOrder(Order order);
}