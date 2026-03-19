using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.ocp
{
    class Case1
    {
        interface ICoolGuy
        {
            void CallCoolGuy();
        }
        class User
        {
            // изменяю члены чтобы можно было управлять через другие классы
            public bool IsSelected {get; }
            public string Image {get; }

            public User(bool isSelected, string image)
            {
                IsSelected = isSelected;
                Image = image;
            }
            public void DrawUser(IEnumerable<IUserFeatureDrawer> featureDrawers)
            {
                // отрисовка присущая всем user 
                DrawBaseUser();

                // проходимся по последоват. способов отрисовки 
                // Если этот способ может отрисовать что-то для user, то рисуем
                foreach (var drawer in featureDrawers)
                {
                    if (drawer.CanDraw(this))
                        drawer.Draw(this);
                }
            }
            private void DrawBaseUser() { }
        }
        interface IUserFeatureDrawer
        {
            bool CanDraw(User user);
            void Draw(User user);
        }
        // До этого для каждой новой отрисовки приходилось бы изменять уже рабочую версию, добавляя новые if и реализации.
        // Сейчас мы можем не изменять имеющийся код, а расширять его. 
        class SelectedUserDrawer : IUserFeatureDrawer
        {
            public bool CanDraw(User user) => user.IsSelected;

            public void Draw(User user)
            {
                DrawEllipseAroundUser();
            }

            private void DrawEllipseAroundUser() { }
        }

        class ImageUserDrawer : IUserFeatureDrawer
        {
            public bool CanDraw(User user) => user.Image != null;

            public void Draw(User user)
            {
                DrawImageOfUser();
            }

            private void DrawImageOfUser() { }
        }

        class CoolGuyDrawer : IUserFeatureDrawer
        {
            public bool CanDraw(User user) => user is ICoolGuy;

            public void Draw(User user)
            {
                DrawCoolGuyGlasses();
            }

            private void DrawCoolGuyGlasses() { }
        }
    }
}
