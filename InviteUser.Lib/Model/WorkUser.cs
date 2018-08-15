using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace InviteUser.Lib.Model
{
    public class WorkUser
    {
        public static void DoWork()
        {
            ServiceUser serviceUser = new ServiceUser();
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("results");
            doc.AppendChild(root);

            foreach (results item in serviceUser.InvokeUser())
            {
                XmlAttribute gender = doc.CreateAttribute("gender");
                gender.InnerText = item.gender;
                root.Attributes.Append(gender);

                XmlElement name = doc.CreateElement("name");
                XmlElement nameFirst = doc.CreateElement("nameFirst");
                XmlElement nameLast = doc.CreateElement("nameLast");
                nameFirst.InnerText = item.name.first;
                nameLast.InnerText = item.name.last;
                name.AppendChild(nameFirst);
                name.AppendChild(nameLast);
                root.AppendChild(name);

                XmlElement cell = doc.CreateElement("cell");
                cell.InnerText = item.cell;
                root.AppendChild(cell);

                XmlElement dob = doc.CreateElement("dob");
                XmlAttribute dobAge = doc.CreateAttribute("dobAge");
                dobAge.InnerText = item.dob.age;
                dob.Attributes.Append(dobAge);
                dob.InnerText = item.dob.date;
                root.AppendChild(dob);

                XmlElement location = doc.CreateElement("location");
                XmlElement city = doc.CreateElement("city");
                XmlElement state = doc.CreateElement("state");
                city.InnerText = item.location.city;
                state.InnerText = item.location.state;
                location.AppendChild(city);
                location.AppendChild(state);
                root.AppendChild(location);
            }
            doc.Save(@"\\dc\Студенты\ПКО\SMP-172.1\МК\" + Guid.NewGuid() + ".xml");
        }

        public static void SortHuman()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(@"\\dc\Студенты\ПКО\SMP-172.1\МК");
            foreach (FileInfo item in dirInfo.GetFiles("*.xml"))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(item.FullName); //полный путь к файлу
                                         //XmlNode node = doc.DocumentElement.GetAttribute("gender");//образаемся к аттрибуту
                                         //if(node.InnerText=="male")
                InfoMoveTo(doc, item.FullName);
                if (doc.DocumentElement.GetAttribute("gender") == "male")
                {
                    item.MoveTo(@"\\dc\Студенты\ПКО\SMP-172.1\МК\Male\" + item.Name);
                }
                else
                {
                    item.MoveTo(@"\\dc\Студенты\ПКО\SMP-172.1\МК\Female\" + item.Name);

                }
            }
        }

        public static void InfoMoveTo(XmlDocument docInfo, string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"\\dc\Студенты\ПКО\SMP-172.1\МК\Report\report.xml");
            XmlElement root = doc.DocumentElement; //возвращает корень
            XmlElement user = doc.CreateElement("user");
            XmlElement FIO = doc.CreateElement("FIO");
            FIO.InnerText = docInfo.SelectSingleNode(@"results/name/nameFirst").InnerText;
            FIO.InnerText += docInfo.SelectSingleNode(@"results/name/nameLast").InnerText;
            user.AppendChild(FIO);

            XmlElement cell = doc.CreateElement("cell");
            cell.InnerText = docInfo.SelectSingleNode(@"results/cell").InnerText;
            user.AppendChild(cell);

            XmlElement location = doc.CreateElement("location");
            location.InnerText = docInfo.SelectSingleNode(@"results/location/city").InnerText;
            location.InnerText += docInfo.SelectSingleNode(@"results/location/state").InnerText;
            user.AppendChild(location);

            XmlElement age = doc.CreateElement("age");
            XmlNode n = docInfo.SelectSingleNode(@"results/dob");
            age.InnerText = n.Attributes[0].InnerText;
            user.AppendChild(age);

            XmlElement Status = doc.CreateElement("status");
            Status.InnerText = docInfo.SelectSingleNode(@"results/status").InnerText;
            user.AppendChild(Status);

            XmlElement UpdateUser = doc.CreateElement("updateUser");
            user.AppendChild(UpdateUser);

            XmlElement fullPath = doc.CreateElement("fullPath");
            fullPath.InnerText = filePath;
            user.AppendChild(fullPath);

            
            root.AppendChild(user);
            doc.Save(@"\\dc\Студенты\ПКО\SMP-172.1\МК\Report\report.xml");
        }
    }
}
