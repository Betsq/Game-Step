using Game_Step.Models;
using Game_Step.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Game_Step.Models.Orders;

namespace Game_Step.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationContext _db;

        public OrderController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult Order(CartOrderViewModel model)
        {
            if (!OrderValidate(model))
                return RedirectToAction("Index", "Cart", model);

            int totalPrice = 0;

            var orderNumber = SetOrderNumber(model);

            foreach (var item in model.Items)
            {

                var game = _db.Games
                    .Include(keys => keys.GameKeys)
                    .Include(price => price.GamePrice)
                    .FirstOrDefault(g => g.Id == item.Key);

                if (game == null)
                    continue;

                if (!ValidateAmountProduct(item.Value, game))
                    return RedirectToAction("Index", "Cart");

                SetPrice(game, item.Value, totalPrice, out var gamePrice, out totalPrice);

                var order = new Order
                {
                    ProductId = game.Id,
                    AmountProduct = item.Value,
                    ProductName = game.Name,
                    ProductPrice = gamePrice,
                    OrderNumber = orderNumber,
                };
                _db.Orders.Add(order);
                SetOrderKeysGame(order, game, item.Value);
            }

            orderNumber.TotalPrice = totalPrice;

            _db.OrderNumbers.Add(orderNumber);
            HttpContext.Session.Remove("CartId");
            HttpContext.Session.Remove("CountOfGoods");
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public OrderNumber SetOrderNumber(CartOrderViewModel model)
        {
            var orderNumber = new OrderNumber
            {
                Email = model.Email,
                UserAgreement = model.UserAgreement,
                PaymentMethod = model.PaymentMethod,
                Promocode = model.Promocode,
                OrderTime = DateTime.Now,
            };

            return (orderNumber);
        }

        public void SetOrderKeysGame(Order order, Game game, int quantity)
        {
            for (var i = 0; i < quantity; i++)
            {
                var key = game.GameKeys[i];
                var orderKeysGame = new OrderKeysGame()
                {
                    Key = key.KeyToGame,
                    Order = order,
                };

                _db.GameKeys.Remove(key);
                _db.OrderKeysGames.Add(orderKeysGame);
            }
        }

        public bool OrderValidate(CartOrderViewModel model)
        {
            if (!ModelState.IsValid)
                return false;

            if (model.UserAgreement == false)
                return false;

            return true;
        }

        public void SetPrice(Game game, int amountProduct, int tPrice, out int gamePrice, out int totalPrice)
        {
            //The variable 'tPrice' is added as the variable 'out totalPrice' does not work without initialization
            totalPrice = tPrice;

            if (game.GamePrice.IsDiscount)
            {
                gamePrice = game.GamePrice.DiscountPrice;

                totalPrice += game.GamePrice.DiscountPrice * amountProduct;
            }
            else
            {
                gamePrice = game.GamePrice.Price;

                totalPrice += game.GamePrice.Price * amountProduct;
            }
        }

        public bool ValidateAmountProduct(int amount, Game game)
        {
            int countKeys = game.GameKeys.Count();

            if (countKeys >= amount)
                return true;

            TempData["Error"] = $"К сожалению недостаточно ключей для игры {game.Name}";
            return false;
        }
    }
}
