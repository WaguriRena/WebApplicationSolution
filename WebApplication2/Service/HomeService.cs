using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Collections.Generic;
using WebApplication2.Models;
using Microsoft.Extensions.Logging;
using static WebApplication2.Models.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication2.Service;


namespace WebApplication2.Service
{
    public class HomeService
    {
        private readonly AppDbContext _context;

        private readonly CommonService _commonService;

        public HomeService(AppDbContext context, CommonService commonService)
        {
            _context = context;
            _commonService = commonService;
        }

        public List<HomeViewModel> GetDataService()
        {
            List<HomeViewModel> list = new List<HomeViewModel>(); //HomeViewModelのリストを作成
            var query = (from m in _context.User //LINQを用いてUserテーブルからデータを取得しリスト化したものをqueryに代入
                         select m).ToList();
            if (query != null)
            {
                for (var i = 0; i < query.Count; i++)
                { //queryの中にあるリストの長さ分だけ繰り返すfor文
                    HomeViewModel homeViewModel = new HomeViewModel(); //HomeViewModelのインスタンスを作成(データを入れるためのモデルクラス。箱のようなもの)
                    homeViewModel.Id = query[i].Id; //queryのi番目のIdをhomeViewModelのIdに代入
                    homeViewModel.Name = query[i].Name;　//queryのi番目のNameをhomeViewModelのNameに代入
                    list.Add(homeViewModel);　//listにhomeViewModelを追加
                }
            }
            return list;　//listを呼び出し元であるHomeControllerクラスのGetData()に返す
        }


        //目標:GetDataServiceTodoメソッドを修正し、データベースコンテキストから取得したデータをhomeViewModelに格納して、それらをリスト化して返す
        //ヒント:GetDataServiceメソッドを参考にしてください
        public List<HomeViewModel> GetDataServiceTodo()
        {
            List<HomeViewModel> list = new List<HomeViewModel>();
            var query = (from m in _context.UserTodo
                         select m).ToList();
            if (query != null)
            {
                for (var i = 0; i < query.Count; i++)
                {
                    HomeViewModel homeViewModel = new HomeViewModel();
                    homeViewModel.Id = query[i].Id;
                    homeViewModel.Name = query[i].Name;
                    list.Add(homeViewModel);
                }
            }
            return list;
        }

        public bool CreateDataService(HomeViewModel viewModel)
        {
            if (viewModel.Name != "")
            {
                User user = new User();
                user.Name = viewModel.Name;
                _context.User.Add(user); //Userテーブルにuserを追加
                _context.SaveChanges();  //データベースに変更を保存
                return true;
            }
            else
            {
                return false;
            }

        }

        //目標:CreateDataServiceTodoメソッドを修正し、データベースコンテキストに新しいデータを追加する。
        //ヒント:CreateDataServiceメソッドを参考にしてください
        public bool CreateDataServiceTodo(HomeViewModel viewModel)
        {
            if (viewModel.Name != "")
            {
                UserTodo user = new UserTodo();
                user.Name = viewModel.Name;
                _context.UserTodo.Add(user);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public HomeViewModel GetDataByIdService(int id)
        {
            var query = (from m in _context.User
                         where m.Id == id
                         select m).ToList();
            if (query != null)
            {
                HomeViewModel homeViewModel = new HomeViewModel();
                homeViewModel.Id = query[0].Id;
                homeViewModel.Name = query[0].Name;
                return homeViewModel;
            }
            else
            {
                return null;
            }
        }

        //目標:GetDataByIdTodoメソッドを修正し、データベースコンテキストから取得したデータをhomeViewModelに格納して返して下さい。
        //ヒント:GetDataByIdメソッドを参考にして下さい。
        public HomeViewModel GetDataByIdServiceTodo(int id)
        {
            var query = (from m in _context.UserTodo
                         where m.Id == id
                         select m).ToList();
            if (query != null)
            {
                HomeViewModel homeViewModel = new HomeViewModel();
                homeViewModel.Id = query[0].Id;
                homeViewModel.Name = query[0].Name;
                return homeViewModel;
            }
            else
            {
                return null;           //4月9日ここまで
            }
        }

        public bool EditDataService(HomeViewModel viewModel)
        {
            if (viewModel.Name != "")
            {
                var query = (from m in _context.User
                             where m.Id == viewModel.Id
                             select m).ToList();
                if (query != null)
                {
                    query[0].Name = viewModel.Name;
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //目標:EditDataServiceTodoメソッドを修正し、データベースコンテキストのデータを編集して下さい。
        //ヒント:EditDataServiceメソッドを参考にしてください。
        public bool EditDataServiceTodo(HomeViewModel viewModel)
        {
            if (viewModel.Name != "")
            {
                var query = (from m in _context.UserTodo
                             where m.Id == viewModel.Id
                             select m).ToList();
                if (query != null)
                {
                    query[0].Name = viewModel.Name;
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
