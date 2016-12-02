using System;
using MySql.Data.MySqlClient;
using System.Data;

namespace FinalProject
{
    class Program
    {
        static String myConnectionString;

        public static void ask()
        {
            int itemIdNo;
            String description;
            int pricePerItem;
            int quantityOnHand;
  
            Console.WriteLine("Please enter your choice from the given options:");
            Console.WriteLine("Enter 1 for adding a new item");
            Console.WriteLine("Enter 2 for Changing the existing item");
            Console.WriteLine("Enter 3 for Deleting an item");
            Console.WriteLine("Enter 4 for Getting list of all the items");
            Console.Write("Enter 5 to exit: ");
            int option = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            switch (option)
            {
                case 1:
                    {
                        Console.Write("Please enter the itemId: ");
                        itemIdNo = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Please enter the description: ");
                        description = Console.ReadLine();

                        Console.Write("Please enter price per Item: ");
                        pricePerItem = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Please enter the quantity of Item: ");
                        quantityOnHand = Convert.ToInt32(Console.ReadLine());

                        addItem(itemIdNo, description, pricePerItem, quantityOnHand);
                        break;
                    }
                case 2:
                    {
                        Console.Write("Please enter the itemId you want to make changes: ");
                        itemIdNo = Convert.ToInt32(Console.ReadLine());
                        changeItem(itemIdNo);
                        break;
                    }
                case 3:

                    {
                        Console.Write("Please enter the itemId for item you want to delete: ");
                        itemIdNo = Convert.ToInt32(Console.ReadLine());
                        deleteItem(itemIdNo);
                        break;
                    }
                case 4:
                    {
                        listAllItems();
                        break;
                    }
                case 5:
                    {
                        Console.Write("Are you sure you want to quit - y/n?: ");
                        String reply = Console.ReadLine();
                        while (!(reply.Equals("n") || reply.Equals("N") || reply.Equals("y") || reply.Equals("Y")))
                        {
                            Console.Write("Please enter the right option - y/n?: ");
                            reply = Console.ReadLine();
                            if (reply.Equals("n") || reply.Equals("N"))
                            {
                                ask();
                            }
                            else if (reply.Equals("y") || reply.Equals("Y"))
                            {
                                break;
                            }
                        }
                        if (reply.Equals("n") || reply.Equals("N"))
                        {
                            ask();
                        }
                        else if (reply.Equals("y") || reply.Equals("Y"))
                        {
                            break;
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Sorry no such option available! Press any key to exit");
                        Console.ReadLine();
                        break;
                    }
            }
        }

        public static void addItem(int itemIdNo, String description, int pricePerItem, int QOH)
        {

            int costPerItem = pricePerItem * QOH;
            Console.WriteLine();
            myConnectionString = "Server=rishikamaroo.crdjyxwi0g2g.us-west-2.rds.amazonaws.com; Database=myDatabase; Uid=rishikamaroo; Pwd=rishikamaroo;";
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            try
            {
               //Console.WriteLine("Adding to database");
                MySqlCommand cmd;
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO `myDatabase`.`Enterprice_List` (`ItemId`, `Description`, `PricePerItem`, `QuantityOnHand`, `Cost`) VALUES (?itemIdNo, ?description, ?pricePerItem, ?QOH, ?costPerItem);";
                cmd.Parameters.AddWithValue("?itemIdNo", itemIdNo);
                cmd.Parameters.AddWithValue("?description", description);
                cmd.Parameters.AddWithValue("?pricePerItem", pricePerItem);
                cmd.Parameters.AddWithValue("?QOH", QOH);
                cmd.Parameters.AddWithValue("?costPerItem", costPerItem);
                //INSERT INTO `myDatabase`.`Enterprice_List` (`ItemId`, `Description`, `PricePerItem`, `QuantityOnHand`, `Cost`) VALUES ('10', 'a', '10', '10', '20');
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    Console.WriteLine("Added!");
                    connection.Close(); 
                }
            }
            Console.Write("Do you want to make any other changes in the database. Press y/Y if yes: ");
            String ResponseToContinue = Console.ReadLine();
            if (ResponseToContinue.Equals("Y") || ResponseToContinue.Equals("y"))
            {
                Console.WriteLine();
                ask();
            }
            else
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
            }
        }

        public static void changeItem(int itemIdNo)
        {
            Console.WriteLine(" ");
            myConnectionString = "Server=rishikamaroo.crdjyxwi0g2g.us-west-2.rds.amazonaws.com; Database=myDatabase; Uid=rishikamaroo; Pwd=rishikamaroo;";
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            try
            {
                MySqlCommand cmd;
                connection.Open();
                cmd = connection.CreateCommand();
                bool moreChanges = true;
                Console.WriteLine("Please enter the number for change");
                Console.WriteLine("Enter 1 for ItemIdNo");
                Console.WriteLine("Enter 2 for Description");
                Console.WriteLine("Enter 3 for Price/Item");
                Console.WriteLine("Enter 4 for Quantity on Hand");
                Console.Write("Enter 5 to exit: ");
                int response = Convert.ToInt32(Console.ReadLine());
                if (response == 1)
                {
                    Console.Write("Please enter the new itemId: ");
                    int newItemIdNo = Convert.ToInt32(Console.ReadLine());
                    cmd.CommandText = "UPDATE `myDatabase`.`Enterprice_List` SET `ItemId`= ?newItemIdNo WHERE `ItemId`= ?itemIdNo;";
                    //UPDATE `myDatabase`.`Enterprice_List` SET `Description`='Basketball kit', `PricePerItem`='40', `QuantityOnHand`='2', `Cost`='80' WHERE `ItemId`='101';
                    cmd.Parameters.AddWithValue("?itemIdNo", itemIdNo);
                    cmd.Parameters.AddWithValue("newItemIdNo", newItemIdNo);
                    cmd.ExecuteNonQuery();
                }
                else if (response == 2)
                {
                    Console.Write("Please enter the new description: ");
                    String description = Console.ReadLine();
                    cmd.CommandText = "UPDATE `myDatabase`.`Enterprice_List` SET `Description`= @description WHERE `ItemId`= @itemIdNo;";
                    cmd.Parameters.AddWithValue("@itemIdNo", itemIdNo);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.ExecuteNonQuery();
                }
                else if (response == 3)
                {
                    Console.Write("Please enter the new price/item: ");
                    int pricePerItem = Convert.ToInt32(Console.ReadLine());
                    int QOH = 0;
                    string sql_users2 = "SELECT `QuantityOnHand` FROM `Enterprice_List` WHERE `ItemId`= ?itemIdNo";
                   
                    MySqlCommand fetchQOH = new MySqlCommand(sql_users2, connection);
                    fetchQOH.Parameters.AddWithValue("?itemIdNo", itemIdNo);
                    object result = fetchQOH.ExecuteScalar();
                    if (result != null) QOH = Convert.ToInt32(result);

                    int newCost = pricePerItem * QOH;
                    cmd.CommandText = "UPDATE `myDatabase`.`Enterprice_List` SET `PricePerItem`= ?pricePerItem, `Cost`= ?Cost  WHERE `ItemId`= ?itemIdNo ;";
                    cmd.Parameters.AddWithValue("?itemIdNo", itemIdNo);
                    cmd.Parameters.AddWithValue("?Cost", newCost);
                    cmd.Parameters.AddWithValue("?pricePerItem", pricePerItem);
                    cmd.ExecuteNonQuery();
                }

                else if (response == 4)
                {
                    Console.Write("Please enter the new quantity: ");
                    int quantityOnHand = Convert.ToInt32(Console.ReadLine());
                    int PPI = 0;
                    string sql_users2 = "SELECT `PricePerItem` FROM `Enterprice_List` WHERE `ItemId`= ?itemIdNo";

                    MySqlCommand fetchPPI = new MySqlCommand(sql_users2, connection);
                    fetchPPI.Parameters.AddWithValue("?itemIdNo", itemIdNo);
                    object result = fetchPPI.ExecuteScalar();
                    if (result != null) PPI = Convert.ToInt32(result);

                    int newCost = PPI * quantityOnHand;
                    cmd.CommandText = "UPDATE `myDatabase`.`Enterprice_List` SET `QuantityOnHand`= ?quantityOnHand, `Cost`= ?Cost WHERE `ItemId`= ?itemIdNo;";
                    cmd.Parameters.AddWithValue("?itemIdNo", itemIdNo);
                    cmd.Parameters.AddWithValue("?Cost", newCost);
                    cmd.Parameters.AddWithValue("?quantityOnHand", quantityOnHand);
                    cmd.ExecuteNonQuery();
                }
                else if (response == 5)
                {
                    moreChanges = false;
                    Console.WriteLine("Will make an exit!");
                    moreChanges = false;
                }
                else
                {
                    moreChanges = false;
                    Console.WriteLine("Invalid option!");
                    moreChanges = false;
                }
                if (moreChanges)
                {
                    Console.Write("Do you want to make more chanes - y/n?: ");
                    String response1 = Console.ReadLine();
                    if (response1.Equals("y") || response1.Equals("Y"))
                    {
                        Console.Write("Please enter the itemId: ");
                        int itemId = Convert.ToInt32(Console.ReadLine());
                        changeItem(itemId);
                    }
                }
                //INSERT INTO `myDatabase`.`Enterprice_List` (`ItemId`, `Description`, `PricePerItem`, `QuantityOnHand`, `Cost`) VALUES ('10', 'a', '10', '10', '20');
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    Console.WriteLine("Updated!");
                    connection.Close();
                }
            }
            Console.Write("Do you want to make any other changes in the database. Press y/Y if yes: ");
            String ResponseToContinue = Console.ReadLine();
            if (ResponseToContinue.Equals("Y") || ResponseToContinue.Equals("y"))
            {
                ask();
            }
            else
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
            }           
        }

        public static void deleteItem(int itemIdNo)
        {
            myConnectionString = "Server=rishikamaroo.crdjyxwi0g2g.us-west-2.rds.amazonaws.com; Database=myDatabase; Uid=rishikamaroo; Pwd=rishikamaroo;";
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            try
            {
                Console.WriteLine("Deleting from database");
                MySqlCommand cmd;
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM `myDatabase`.`Enterprice_List` WHERE `ItemId`= ?itemIdNo;";
                cmd.Parameters.AddWithValue("?itemIdNo", itemIdNo);
                //INSERT INTO `myDatabase`.`Enterprice_List` (`ItemId`, `Description`, `PricePerItem`, `QuantityOnHand`, `Cost`) VALUES ('10', 'a', '10', '10', '20');
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    Console.WriteLine("Deleted!");
                    connection.Close();
                }
            }
            ask();
        }

        public static void listAllItems()
        {
            myConnectionString = "Server=rishikamaroo.crdjyxwi0g2g.us-west-2.rds.amazonaws.com; Database=myDatabase; Uid=rishikamaroo; Pwd=rishikamaroo;";
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            try
            {
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                string stm = "SELECT * FROM myDatabase.Enterprice_List;";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                int i = 1;
                Sample sample = new Sample();
                Console.Clear();
                sample.origRow = Console.CursorTop;
                sample.origCol = Console.CursorLeft;
                Console.WriteLine("Item#  ItemID  Description           Price  QOH  Cost");
                Console.WriteLine("-----  ------  --------------------  -----  ---  ----");
                int lineNo = 2;
                while (rdr.Read())
                {
                    sample.WriteAt(i+"", 0, lineNo);
                    sample.WriteAt(rdr.GetInt32(0) + "", 7, lineNo);
                    sample.WriteAt(rdr.GetString(1), 15, lineNo);
                    sample.WriteAt(rdr.GetInt32(2) + "", 37, lineNo);
                    sample.WriteAt(rdr.GetInt32(3) + "", 44, lineNo);
                    sample.WriteAt(rdr.GetInt32(3) + "", 49, lineNo);
                    lineNo++;
                    i++;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }
            Console.ReadLine();
            ask();
        }

        static void Main(string[] args)
        {   
            //Console Settings
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.SetWindowSize(80,40);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Title = "Enterprice List";

            Program prog = new Program();
            //Going to program ask 
            ask();
        }
    }
}