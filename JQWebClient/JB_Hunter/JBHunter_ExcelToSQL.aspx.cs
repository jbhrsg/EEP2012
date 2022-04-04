using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data.Common;
using System.Data;
using System.Configuration;
//using InfoRemoteModule;
using System.ComponentModel;
using System.Data;

public partial class exceltosql : System.Web.UI.Page 
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
   
  
    }
        private void InitializeComponent()
    {
       
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(exceltosql));

    }

    public override void ProcessRequest(HttpContext context)
    {
        if (!JqHttpHandler.ProcessRequest(context))
        {
            base.ProcessRequest(context);
        }
    }
   
    protected void Button1_Click(object sender, EventArgs e)
    {
        int errorcount = 0;//记录错误信息条数  
        int insertcount = 0;//记录插入成功条数  
        int updatecount = 0;//记录更新信息条数  
       
        string exceldowm = @"file";
        string Loginuser = Request.Form["Hidden1"];
        
       

        try
        {
            //建立目錄
            

            SqlConnection connect;
            //建立連線字串
            string con = ConfigurationManager.ConnectionStrings["Hunter"].ToString();
            connect = new SqlConnection(con);
          
        }

        catch
        {
            Response.Write("<script>parent.location.href='ErrorMessage.aspx'</script>");
        }

        //指定上傳路徑
        string target = @"file" + @"\";
        //取的檔案名稱
        string filename = FileUpload1.FileName.ToString();
        string Photoname = FileUpload2.FileName.ToString();
        string Worksname1 = FileUpload3.FileName.ToString();
        string Worksname2 = FileUpload4.FileName.ToString();
        string Worksname3 = FileUpload5.FileName.ToString();
        //Excel檔案的實體路徑
        string path = Server.MapPath(target + filename);
        string pathc;
        //檔上傳回指定目錄
        if (FileUpload1.FileName=="")
        {
            Response.Write("<script>alert('請確認上傳檔案')</script>");
        }
        else
        {
             string ext = System.IO.Path.GetExtension(filename);
             string photo = System.IO.Path.GetExtension(Photoname);
             
           if(ext != ".xls" && ext != ".xlsx" && ext != ".xlsm")  
            {
                Response.Write("<script>alert('上傳履歷格式不符')</script>");
                return;
            }
           if (photo != ".jpg" && photo != ".jpge" && photo != ".png" && photo != "" && photo != null)
           {
               Response.Write("<script>alert('上傳圖片格式不符')</script>");
               return;
           }
           for (int z = 1; z <= 3; z++)
           {
               string work = System.IO.Path.GetExtension("Worksname" + z);
               if (work != ".zip" && work != ".rar" && work != "" && work != null && work!=".z7")
               {
                   Response.Write("<script>alert('上傳作品請壓縮成壓縮檔格式')</script>");
                   
                   return;
                   break;
                   
               }
           }
               FileUpload1.SaveAs(path);
            //Excel的OLEDB ConnectionString 


           string ExcelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +  path + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=2\"";
            OleDbConnection ExcelCn = new OleDbConnection(ExcelConnectionString);
            OleDbCommand ExcelCmd = new OleDbCommand();
            DbDataReader ExcelDr = null;
            //OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [工作表1$]", ExcelConnectionString);
          
            ExcelCmd.CommandText = "Select * from [工作表1$]";
            //抓取Excel資料的SQL指令 
            ExcelCmd.CommandType = CommandType.Text;
            ExcelCmd.Connection = ExcelCn;
            
            ExcelCmd.Connection.Open();
            ExcelDr = ExcelCmd.ExecuteReader(CommandBehavior.CloseConnection);
            DataTable dt = new DataTable();
            dt.Load(ExcelDr);
            SqlConnection cn = new SqlConnection();
            //設定資料庫Connect物件       

            //設定資料庫Connection連接 
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["Hunter"].ToString();
            cn.Open();
            
            string NameC = dt.Rows[0][1].ToString();
            string NameE = dt.Rows[0][2].ToString();
            string Birthdate= dt.Rows[0][3].ToString();
            string Email1= dt.Rows[0][4].ToString();
            string Email2 = dt.Rows[0][5].ToString();
            string Gender = dt.Rows[0][6].ToString();
            string Height = dt.Rows[0][7].ToString();
            string Weight = dt.Rows[0][8].ToString();
            string BloodType = dt.Rows[0][9].ToString();
            string MarriageStatus = dt.Rows[0][10].ToString();
            string Country1 = dt.Rows[0][11].ToString();
            string ArmyStatus = dt.Rows[0][12].ToString();
            string MobileNo1 = dt.Rows[0][13].ToString();
            string MobileArea1 = dt.Rows[0][14].ToString();
            string MobileNo2 = dt.Rows[0][15].ToString();
            string MobileArea2 = dt.Rows[0][16].ToString();
            string PhoneNo = dt.Rows[0][17].ToString();
            string DrivingLicense = dt.Rows[0][18].ToString();
            string Traffic = dt.Rows[0][19].ToString();
            string IMType = dt.Rows[0][20].ToString();
            string IMAccount = dt.Rows[0][21].ToString();
            string Address1 = dt.Rows[0][22].ToString();
            string Address2 = dt.Rows[0][23].ToString();
            string ExpPay = dt.Rows[0][24].ToString();
            string ExpDutyDt = dt.Rows[0][25].ToString();
            string ExpDutyArea = dt.Rows[0][26].ToString();
            string EduID1 = dt.Rows[0][27].ToString();
            string SchoolName1 = dt.Rows[0][28].ToString();
            string SchoolArea1 = dt.Rows[0][29].ToString();
            string EduSubject1 = dt.Rows[0][30].ToString();
            string Department1 = dt.Rows[0][31].ToString();
            string GraduateYear1 = dt.Rows[0][32].ToString();
            string GradStatus1 = dt.Rows[0][33].ToString();
            string EduID2 = dt.Rows[0][34].ToString();
            string SchoolName2 = dt.Rows[0][35].ToString();
            string SchoolArea2 = dt.Rows[0][36].ToString();
            string EduSubject2 = dt.Rows[0][37].ToString();
            string Department2 = dt.Rows[0][38].ToString();
            string GraduateYear2 = dt.Rows[0][39].ToString();
            string GradStatus2 = dt.Rows[0][40].ToString();
            string EduID3 = dt.Rows[0][41].ToString();
            string SchoolName3 = dt.Rows[0][42].ToString();
            string SchoolArea3 = dt.Rows[0][43].ToString();
            string EduSubject3 = dt.Rows[0][44].ToString();
            string Department3 = dt.Rows[0][45].ToString();
            string GraduateYear3 = dt.Rows[0][46].ToString();
            string GradStatus3 = dt.Rows[0][47].ToString();
            string FamRole1=dt.Rows[3][1].ToString();
            string FamName1=dt.Rows[3][2].ToString();
            string FamBirthday1=dt.Rows[3][3].ToString();
            string FamOccup1 = dt.Rows[3][4].ToString();
            string FamRole2 = dt.Rows[3][5].ToString();
            string FamName2 = dt.Rows[3][6].ToString();
            string FamBirthday2 = dt.Rows[3][7].ToString();
            string FamOccup2 = dt.Rows[3][8].ToString(); 
            string FamRole3 = dt.Rows[3][9].ToString();
            string FamName3 = dt.Rows[3][10].ToString();
            string FamBirthday3 = dt.Rows[3][11].ToString();
            string FamOccup3 = dt.Rows[3][12].ToString();
            string FamRole4 = dt.Rows[3][13].ToString();
            string FamName4 = dt.Rows[3][14].ToString();
            string FamBirthday4 = dt.Rows[3][15].ToString();
            string FamOccup4 = dt.Rows[3][16].ToString();
            string FamRole5 = dt.Rows[3][17].ToString();
            string FamName5 = dt.Rows[3][18].ToString();
            string FamBirthday5 = dt.Rows[3][19].ToString();
            string FamOccup5 = dt.Rows[3][20].ToString();
            string FamRole6 = dt.Rows[3][21].ToString();
            string FamName6 = dt.Rows[3][22].ToString();
            string FamBirthday6 = dt.Rows[3][23].ToString();
            string FamOccup6 = dt.Rows[3][24].ToString();
            string MsOffice=dt.Rows[22][1].ToString();
            string Erp=dt.Rows[22][2].ToString();
            string CadCam=dt.Rows[22][3].ToString();
            string Other = dt.Rows[22][4].ToString();
            string RecName1=dt.Rows[25][1].ToString();
            string RecFirm1=dt.Rows[25][2].ToString();
            string RecDep1=dt.Rows[25][3].ToString();	
            string RecTitle1=dt.Rows[25][4].ToString();
            string RecTel1 = dt.Rows[25][5].ToString();
            string RecName2 = dt.Rows[25][6].ToString();
            string RecFirm2 = dt.Rows[25][7].ToString();
            string RecDep2 = dt.Rows[25][8].ToString();
            string RecTitle2 = dt.Rows[25][9].ToString();
            string RecTel2 = dt.Rows[25][10].ToString();
            string TrainCreden = dt.Rows[28][1].ToString();
            string ChnBio= dt.Rows[28][2].ToString();
            string EngBio = dt.Rows[28][3].ToString();
            string WorkItem1=dt.Rows[31][1].ToString();
            string Seniority1 = dt.Rows[31][2].ToString();
            string WorkItem2 = dt.Rows[31][3].ToString();
            string Seniority2 = dt.Rows[31][4].ToString();
            string WorkItem3 = dt.Rows[31][5].ToString();
            string Seniority3 = dt.Rows[31][6].ToString();
            string WorkItem4 = dt.Rows[31][7].ToString();
            string Seniority4 = dt.Rows[31][8].ToString();
            string WorkItem5 = dt.Rows[31][9].ToString();
            string Seniority5 = dt.Rows[31][10].ToString();
            string WorkItem6 = dt.Rows[31][11].ToString();
            string Seniority6 = dt.Rows[31][12].ToString();
            string WorkItem7 = dt.Rows[31][13].ToString();
            string Seniority7 = dt.Rows[31][14].ToString();
            string WorkItem8 = dt.Rows[31][15].ToString();
            string Seniority8 = dt.Rows[31][16].ToString();
            string CreateBy = Loginuser;
            string CreateDate = DateTime.Now.ToString();





            //string CreateBy = ;

            string GGH = Server.MapPath( target);
           // Birth_date = String.Format("yyyy-MM-dd ", Birth_date);
            System.IO.Directory.CreateDirectory(GGH + NameC + Birthdate);
            //string photodown = exceldowm + @"\" +  + @"\" + NameC + Birthdate + photo;
            
           
            //target = target + NameC + Birthdate +@"\";
            //pathc = GGH + NameC + Birthdate +@"\"+ NameC + Birthdate + ext;
            //FileUpload1.SaveAs(pathc);
            //if (photo != ""){
            //pathc = GGH + NameC + Birthdate + @"\" + NameC + Birthdate + photo;
            //FileUpload2.SaveAs(pathc);
            //}
            //else
            //{ photodown = " "; 
            //}
            //if (FileUpload3.FileName != "")
            //{
            //    FileUpload3.SaveAs(GGH + NameC + Birthdate + @"\" + FileUpload3.FileName);
            //    Worksname1 = exceldowm + @"\"+ NameC + Birthdate + @"\" + FileUpload3.FileName;
            //}
            //else
            //{
            //    Worksname1 = " ";
            //}
            //if (FileUpload4.FileName != "")
            //{
            //    FileUpload4.SaveAs(GGH + NameC + Birthdate + @"\" + FileUpload4.FileName);
            //    Worksname2 = exceldowm + @"\" + NameC + Birthdate + @"\" + FileUpload4.FileName;
            //}
            //else
            //{
            //    Worksname2 = " ";
            //}
            //if (FileUpload5.FileName != "")
            //{
            //    FileUpload5.SaveAs(GGH + NameC + Birthdate + @"\" + FileUpload5.FileName);
            //    Worksname3 = exceldowm + @"\" + NameC + Birthdate + @"\" + FileUpload5.FileName;
            //}
            //else
            //{
            //    Worksname3 = " ";
            //}
            //exceldowm = exceldowm + @"\" + NameC + Birthdate + @"\" + NameC + Birthdate + ext;   

            if (NameC != "" && NameE != "" && Birthdate != "")
            {
                SqlCommand selectcmd = new SqlCommand("select  count(UserID) from HUT_User where NameC='" + NameC + "' and Birthday ='" + Birthdate + "' group by UserID" , cn);
                SqlCommand selectUScmd = new SqlCommand("select MAX(UserID) from HUT_User", cn);
                SqlCommand sUserID = new SqlCommand("select  UserID from HUT_User where NameC='" + NameC + "' and Birthday ='" + Birthdate + "'", cn);
                string User = Convert.ToString(selectUScmd.ExecuteScalar());
                string sUser = Convert.ToString(sUserID.ExecuteScalar());
                int count = Convert.ToInt32(selectcmd.ExecuteScalar());
                if (count != 0)
                {
                    string UPdateHutUser = "UPDATE HUT_User SET NameC='" + NameC + "',NameE='" + NameE + "',Birthday='" + Birthdate + "', Email1 = '" + Email1 + "' , Email2='" + Email2 + "' , Gender='" + Gender + "' , Height='" + Height + "' , Weight='" + Weight + "' , BloodType='" + BloodType + "' , MarriageStatus='" + MarriageStatus + "' , Country1='" + Country1 + "' ,  ArmyStatus='" + ArmyStatus + "' , MobileNo1='" + MobileNo1 + "' ,MobileArea1='" + MobileArea1 + "', MobileNo2='" + MobileNo2 + "',MobileArea2='" + MobileArea2 + "' , PhoneNo='" + PhoneNo + "' ,DrivingLicense='" + DrivingLicense + "',Traffic='" + Traffic + "', IMType='" + IMType + "' , IMAccount='" + IMAccount + "' , Address1='" + Address1 + "' , Address2='" + Address2 + "' , ExpPay='" + ExpPay + "' , ExpDutyDt='" + ExpDutyDt + "' , ExpDutyArea='" + ExpDutyArea + "', EduID1='" + EduID1 + "', SchoolName1='" + SchoolName1 + "' , SchoolArea1='" + SchoolArea1 + "',EduSubject1='" + EduSubject1 + "' , Department1='" + Department1 + "' , GraduateYear1='" + GraduateYear1 + "',GradStatus1='" + GradStatus1 + "' , EduID2='" + EduID2 + "', SchoolName2='" + SchoolName2 + "', SchoolArea2='" + SchoolArea2 + "',EduSubject2='" + EduSubject2 + "' , Department2 ='" + Department2 + "', GraduateYear2='" + GraduateYear2 + "',GradStatus2='" + GradStatus2 + "',EduID3='" + EduID3 + "', SchoolName3='" + SchoolName3 + "' , SchoolArea3='" + SchoolArea3 + "',EduSubject3='" + EduSubject3 + "' , Department3='" + Department3 + "' , GraduateYear3='" + GraduateYear3 + "',GradStatus3='"+GradStatus3+"',LastUpdateby='"+CreateBy+"'  , LastUpdateDate=GetDate() where UserID='" + sUser + "'";
                    string UPdateHutUserFam = "UPDATE HUT_UserFam Set FamRole1='" + FamRole1 + "',FamName1='" + FamName1 + "',FamBirthday1='" + FamBirthday1 + "',FamOccup1='" + FamOccup1+ "',FamRole2='" + FamRole2 + "',FamName2='" + FamName2+ "', FamBirthday2 = '" + FamBirthday2 + "' , FamOccup2='" + FamOccup2 + "' , FamRole3='" + FamRole3 + "' , FamName3='" +FamName3 + "' , FamBirthday3='" + FamBirthday3 + "' , FamOccup3='" + FamOccup3 + "' , FamRole4='" + FamRole4 + "' , FamName4	='" + FamName4	 + "' ,  FamBirthday4='" + FamBirthday4 + "' ,FamOccup4='" + FamOccup4 + "' ,FamRole5='" + FamRole5 + "', FamName5='" + FamName5 + "',FamBirthday5='" + FamBirthday5 + "' ,FamOccup5='" + FamOccup5 + "' ,FamRole6='" + FamRole6 + "' , FamName6='" + FamName6 + "' , FamBirthday6='" + FamBirthday6 + "' , FamOccup6='" + FamOccup6 + "'where UserID='" + sUser+"'";
                    string UPdateHutUserGuide = "UPDATE HUT_UserGuide Set WorkItem1='" + WorkItem1 + "',Seniority1='" + Seniority1 + "',WorkItem2='" + WorkItem2 + "',Seniority2='" + Seniority2 + "',WorkItem3='" + WorkItem3 + "',Seniority3='" + Seniority3 + "', WorkItem4 = '" + WorkItem4 + "' , Seniority4='" + Seniority4 + "' ,WorkItem5='" + WorkItem5 + "' ,Seniority5='" + Seniority5 + "' , WorkItem6='" + WorkItem6 + "' , Seniority6='" + Seniority6 + "' , WorkItem7='" + WorkItem7 + "' , Seniority7	='" + Seniority7 + "' ,  WorkItem8='" + WorkItem8 + "' ,Seniority8='" + Seniority8 + "'where UserID='" + sUser + "'";
                    string UPdateHutUserOther = "UPDATE HUT_UserOther Set RecName1='" + RecName1 + "',RecFirm1='" + RecFirm1 + "',RecDep1='" + RecDep1 + "',RecTitle1='" + RecTitle1 + "',RecTel1='" + RecTel1 + "',RecName2='" + RecName2 + "',RecFirm2 = '" + RecFirm2 + "' , RecDep2='" + RecDep2 + "' ,RecTitle2='" + RecTitle2 + "' ,RecTel2='" + RecTel2 + "'where UserID='" + sUser + "'";
                    string UPdateHutUserSkill = "UPDATE HUT_UserItSkill Set MsOffice='" + MsOffice + "',Erp='" + Erp + "',CadCam='" + CadCam + "',Other='" + Other + "' where UserID = '" + sUser + "'";
                    string UPdateHutUserTrBio = "UPDATE HUT_UserTrBio Set TrainCreden='" + TrainCreden + "',ChnBio='" + ChnBio + "',EngBio='" + EngBio + "' where UserID='" + sUser + "'";
                    SqlCommand updatecmd = new SqlCommand(UPdateHutUser+" "+UPdateHutUserFam+" "+UPdateHutUserGuide+" "+UPdateHutUserOther+" "+UPdateHutUserSkill+" "+UPdateHutUserTrBio, cn);
                    updatecmd.ExecuteNonQuery();
                    for (int i = 14; i <= 20; i++)
                    {
                        string LangID = dt.Rows[i][1].ToString();
                        //string LangType = dt.Rows[i][2].ToString();
                        string ListenLevel = dt.Rows[i][2].ToString();
                        string SayLevel = dt.Rows[i][3].ToString();
                        string ReadLevel = dt.Rows[i][4].ToString();
                        string WriteLevel = dt.Rows[i][5].ToString();
                        string TypeWrite = dt.Rows[i][6].ToString();
                        string LangLicense = dt.Rows[i][7].ToString();
                        string LicenseScore = dt.Rows[i][8].ToString();
                        int z = i - 13;
                        if (i == 14)
                        {
                            string DelectHutUserLang = "DELETE FROM HUT_UserLang where UserID='" + sUser + "'";
                            SqlCommand delect1cmd = new SqlCommand(DelectHutUserLang, cn);
                            delect1cmd.ExecuteNonQuery();
                        }
                      
                        string UPdateHutUserLang = "Insert into HUT_UserLang (UserID,LangID,ListenLevel,SayLevel,ReadLevel,WriteLevel,TypeWrite,LangLicense,LicenseScore)values('" + User + "','" + LangID + "','" + ListenLevel + "','" + SayLevel + "','" + ReadLevel + "','" + WriteLevel + "','" + TypeWrite + "','" + LangLicense + "','" + LicenseScore + "')";
                       
                        SqlCommand update1cmd = new SqlCommand(UPdateHutUserLang, cn);
                       
                        update1cmd.ExecuteNonQuery();
                    }
                    for (int i = 6; i <= 11; i++) 
                    {
                        string ComID = dt.Rows[i][1].ToString();
                        string ComName = dt.Rows[i][2].ToString();
                        string ComCountry = dt.Rows[i][3].ToString();
                        string ComArea = dt.Rows[i][4].ToString();
                        string ComCount = dt.Rows[i][5].ToString();
                        string ComIndustry = dt.Rows[i][6].ToString();
                        string Industryname = dt.Rows[i][7].ToString();
                        string ComItem = dt.Rows[i][8].ToString();
                        string AdvisorTitle = dt.Rows[i][9].ToString();
                        string DutyDept = dt.Rows[i][10].ToString();
                        string DutyTitle = dt.Rows[i][11].ToString();
                        string DutyStartDate = dt.Rows[i][12].ToString();
                        string DutyEndDate = dt.Rows[i][13].ToString();
                        string DutySalary = dt.Rows[i][14].ToString();
                        string SalaryCurrency = dt.Rows[i][15].ToString();
                        string DutyBonus = dt.Rows[i][16].ToString();
                        string LeaveReason = dt.Rows[i][17].ToString();
                        string DutyContent = dt.Rows[i][18].ToString();
                        string DutyPerform = dt.Rows[i][19].ToString();
                        string DutyPromo = dt.Rows[i][20].ToString();
                        string LastUpdateby = Request.Form["Hidden1"];
                        string LastUpdateDate = DateTime.Now.ToString();
                        int z = i - 5;
                        string selectUseCareer = "select CreateDate, CreateBy From HUT_UseCareer Where UserID='" + sUser + "'and ComID='" + ComID + "'";
                        SqlCommand select1cmd = new SqlCommand(selectUseCareer, cn);
                        SqlDataReader select1Reader = select1cmd.ExecuteReader();
                        select1Reader.Read();
                        string CreateByOld = select1Reader["CreateBy"].ToString();
                        string CreateDateOld = select1Reader["CreateDate"].ToString();
                        select1Reader.Close();
                        string deleteUseCareer = "Delete From HUT_UseCareer where UserID='" + sUser + "'and ComID='" + ComID + "'";
                        SqlCommand delect1cmd = new SqlCommand(deleteUseCareer, cn);
                        delect1cmd.ExecuteNonQuery();
                        string insertUsecareer = "insert into HUT_UseCareer(UserID,ComID,ComName,ComCountry,ComArea,ComCount,IndustryID,IndustryName ,ComItem,AdvisorTitle,DutyDept,DutyTitle,DutyStartDate,DutyEndDate,DutySalary,SalaryCurrency,DutyBonus,LeaveReason,DutyContent,DutyPerform,DutyPromo,CreateBy,CreateDate,LastUpdateby,LastUpdateDate)values('" + User + "','" + ComID + "','" + ComName + "','" + ComCountry + "','" + ComArea + "','" + ComCount + "','" + ComIndustry + "','" + Industryname + "','" + ComItem + "','" + AdvisorTitle + "','" + DutyDept + "','" + DutyTitle + "','" + DutyStartDate + "','" + DutyEndDate + "','" + DutySalary + "','" + SalaryCurrency + "','" + DutyBonus + "','" + LeaveReason + "','" + DutyContent + "','" + DutyPerform + "','" + DutyPromo + "','" + CreateByOld + "','" + CreateDateOld + "','" + LastUpdateby + "','" + LastUpdateDate + "')";
                        SqlCommand insertUsecareercmd = new SqlCommand(insertUsecareer, cn);
                        insertUsecareercmd.ExecuteNonQuery();
                        string ComName1 = dt.Rows[i + 1][2].ToString();
                        if ((ComName1 == " ") || (ComName1 == null))
                        {
                            break;
                        }
                   
                    }

                }

                else
                {
                    
                    string ID = User;
                    string year = ID.Substring(0, 4);
                    string month = ID.Substring(4, 2);
                    string fff = ID.Substring(6,3);
                   int num = Int16.Parse(ID.Substring(6,3)); 
                    string yearNow = Convert.ToString(DateTime.Now.Year);
                    string monthNow = int.Parse(DateTime.Now.Month.ToString()).ToString("00");
                    if (year != yearNow || month != monthNow)
                    {
                        User = yearNow + monthNow + "001";
                    }
                    else {
                        num=num+1;
                        string numm=num.ToString("000");
                        User = year + month + numm;
                    
                    }
                    System.IO.Directory.CreateDirectory(GGH + User);
                    string photodown = exceldowm + @"\" + User +@"\" + User+ photo;


                    target = target + NameC + Birthdate + @"\";
                    pathc = GGH + User + @"\" + User + ext;
                    FileUpload1.SaveAs(pathc);
                    if (photo != "")
                    {
                        pathc = GGH + User + @"\" + User + photo;
                        FileUpload2.SaveAs(pathc);
                    }
                    else
                    {
                        photodown = " ";
                    }
                    if (FileUpload3.FileName != "")
                    {
                        FileUpload3.SaveAs(GGH + User + @"\" + FileUpload3.FileName);
                        Worksname1 = exceldowm + @"\" + User + @"\" + FileUpload3.FileName;
                    }
                    else
                    {
                        Worksname1 = " ";
                    }
                    if (FileUpload4.FileName != "")
                    {
                        FileUpload4.SaveAs(GGH + User + @"\" + FileUpload4.FileName);
                        Worksname2 = exceldowm + @"\" + User + @"\" + FileUpload4.FileName;
                    }
                    else
                    {
                        Worksname2 = " ";
                    }
                    if (FileUpload5.FileName != "")
                    {
                        FileUpload5.SaveAs(GGH + User + @"\" + FileUpload5.FileName);
                        Worksname3 = exceldowm + @"\" + User + @"\" + FileUpload5.FileName;
                    }
                    else
                    {
                        Worksname3 = " ";
                    }
                    exceldowm = exceldowm + @"\" + User + @"\" + User + ext;   

                    for(int i= 6 ;i<=11;i++)
                    {
                        string ComID = dt.Rows[i][1].ToString();
                        string ComName=dt.Rows[i][2].ToString();
                        string ComCountry=dt.Rows[i][3].ToString();
                        string ComArea=dt.Rows[i][4].ToString();
                        string ComCount=dt.Rows[i][5].ToString();
                        string ComIndustry=dt.Rows[i][6].ToString();
                        string Industryname = dt.Rows[i][7].ToString();
                        string ComItem=dt.Rows[i][8].ToString();
                        string AdvisorTitle=dt.Rows[i][9].ToString();
                        string DutyDept=dt.Rows[i][10].ToString();
                        string DutyTitle=dt.Rows[i][11].ToString();
                        string DutyStartDate=dt.Rows[i][12].ToString();
                        string DutyEndDate=dt.Rows[i][13].ToString();
                        string DutySalary=dt.Rows[i][14].ToString();
                        string SalaryCurrency=dt.Rows[i][15].ToString();
                        string DutyBonus=dt.Rows[i][16].ToString();
                        string LeaveReason=dt.Rows[i][17].ToString();
                        string DutyContent = dt.Rows[i][18].ToString();
                        string DutyPerform = dt.Rows[i][19].ToString();
                        string DutyPromo=dt.Rows[i][20].ToString();
                        
                        int z = i - 5;
                        string insertUsecareer = "insert into HUT_UseCareer(UserID,ComID,ComName,ComCountry,ComArea,ComCount,IndustryID,IndustryName ,ComItem,AdvisorTitle,DutyDept,DutyTitle,DutyStartDate,DutyEndDate,DutySalary,SalaryCurrency,DutyBonus,LeaveReason,DutyContent,DutyPerform,DutyPromo,CreateBy,CreateDate,LastUpdateby,LastUpdateDate)values('" + User + "','" + ComID + "','" + ComName + "','" + ComCountry + "','" + ComArea + "','" + ComCount + "','" + ComIndustry + "','" + Industryname + "','" + ComItem + "','" + AdvisorTitle + "','" + DutyDept + "','" + DutyTitle + "','" + DutyStartDate + "','" + DutyEndDate + "','" + DutySalary + "','" + SalaryCurrency + "','" + DutyBonus + "','" + LeaveReason + "','" + DutyContent + "','" + DutyPerform + "','" + DutyPromo + "','" + CreateBy + "','" + CreateDate + "','" + CreateBy + "','" + CreateDate + "')";
                        SqlCommand insertUsecareercmd = new SqlCommand(insertUsecareer, cn);
                        insertUsecareercmd.ExecuteNonQuery();
                        string ComName1 = dt.Rows[i+1][2].ToString();
                        if ((ComName1 == " ") || (ComName1 == null))
                        {
                            break;
                        }
                    }
                    for (int i = 14; i <= 20; i++)
                    {
                        string LangID = dt.Rows[i][1].ToString();
                        string ListenLevel = dt.Rows[i][2].ToString();
                        string SayLevel = dt.Rows[i][3].ToString();
                        string ReadLevel = dt.Rows[i][4].ToString();
                        string WriteLevel = dt.Rows[i][5].ToString();
                        string TypeWrite = dt.Rows[i][6].ToString();
                        string LangLicense = dt.Rows[i][7].ToString();
                        string LicenseScore = dt.Rows[i][8].ToString();
                        int z = i - 13;
                        string InsertUserLang = "Insert into HUT_UserLang (UserID,LangID,ListenLevel,SayLevel,ReadLevel,WriteLevel,TypeWrite,LangLicense,LicenseScore)values('" + User + "','" + LangID + "','"  + ListenLevel + "','" + SayLevel + "','" + ReadLevel + "','" + WriteLevel + "','" + TypeWrite + "','" + LangLicense + "','" + LicenseScore + "')";
                        SqlCommand Insert1cmd = new SqlCommand(InsertUserLang, cn);
                        Insert1cmd.ExecuteNonQuery();
                    }
                   // string c = "C:\\Program Files (x86)\\Infolight\\EEP2012\\JQWebClient\\JB_Hunter\\file\\nsonsoeen1985-05-05\\nsonsoeen1985-05-05.xls";
                    string InsertUserFar = "Insert into HUT_UserFam(UserID,FamRole1,FamName1,FamBirthday1,FamOccup1,FamRole2,FamName2,FamBirthday2,FamOccup2,FamRole3,FamName3,FamBirthday3,FamOccup3,FamRole4,FamName4,FamBirthday4,FamOccup4,FamRole5,FamName5,FamBirthday5,FamOccup5,FamRole6,FamName6,FamBirthday6,FamOccup6,CreateBy,CreateDate )values('" + User + "','" + FamRole1 + "','" + FamName1 + "','" + FamBirthday1 + "','" + FamOccup1 + "','" + FamRole2 + "','" + FamName2 + "','" + FamBirthday2 + "','" + FamOccup2 + "','" + FamRole3 + "','" + FamName3 + "','" + FamBirthday3 + "','" + FamOccup3 + "','" + FamRole4 + "','" + FamName4 + "','" + FamBirthday4 + "','" + FamOccup4 + "','" + FamRole5 + "','" + FamName5 + "','" + FamBirthday5 + "','" + FamOccup5 + "','" + FamRole6 + "','" + FamName6 + "','" + FamBirthday6 + "','" + FamOccup6 +"','"+CreateBy+"','"+CreateDate+ "')";
                    string InsertUser = "Insert into HUT_User(UserID, NameC , NameE,Birthday,Email1,Email2,Gender,Height,Weight,BloodType,MarriageStatus,Country1,ArmyStatus,MobileNo1,MobileArea1,MobileNo2,MobileArea2,PhoneNo,DrivingLicense,Traffic,IMType,IMAccount,Address1,Address2,ExpPay,ExpDutyDt,ExpDutyArea,EduID1,SchoolName1,SchoolArea1,EduSubject1,Department1,GraduateYear1,GradStatus1,EduID2,SchoolName2,SchoolArea2,EduSubject2,Department2,GraduateYear2,GradStatus2,EduID3,SchoolName3,SchoolArea3,EduSubject3,Department3,GraduateYear3,GradStatus3,DocFile1,DocFile2,DocFile3,PhotoFile,ExcelFile,CreateBy,CreateDate,LastUpdateby,LastUpdateDate) values('" + User + "','" + NameC + "','" + NameE + "','" + Birthdate + "','" + Email1 + "','" + Email2 + "','" + Gender + "','" + Height + "','" + Weight + "','" + BloodType + "','" + MarriageStatus + "','" + Country1 + "','" + ArmyStatus + "','" + MobileNo1 + "','" + MobileArea1 + "','" + MobileNo2 + "','" + MobileArea2 + "','" + PhoneNo + "','" + DrivingLicense + "','" + Traffic + "','" + IMType + "','" + IMAccount + "','" + Address1 + "','" + Address2 + "','" + ExpPay + "','" + ExpDutyDt + "','" + ExpDutyArea + "','" + EduID1 + "','" + SchoolName1 + "','" + SchoolArea1 + "','" + EduSubject1 + "','" + Department1 + "','" + GraduateYear1 + "','" + GradStatus1 + "','" + EduID2 + "','" + SchoolName2 + "','" + SchoolArea2 + "','" + EduSubject2 + "','" + Department2 + "','" + GraduateYear2 + "','" + GradStatus2 + "','" + EduID3 + "','" + SchoolName3 + "','" + SchoolArea3 + "','" + EduSubject3 + "','" + Department3 + "','" + GraduateYear3 + "','" + GradStatus3 + "','" + Worksname1 + "','" + Worksname2 + "','" + Worksname3 + "','" + photodown + "','" + exceldowm + "','" + CreateBy + "',GetDate(),'" + CreateBy + "',GetDate()) ";
                    string InsertUserGuide = "Insert into HUT_UserGuide(UserID,WorkItem1,Seniority1,WorkItem2,Seniority2,WorkItem3,Seniority3,WorkItem4,Seniority4,WorkItem5,Seniority5,WorkItem6,Seniority6,WorkItem7,Seniority7,WorkItem8,Seniority8)values('"+User+"','"+ WorkItem1 + "','" + Seniority1 + "','" + WorkItem2 + "','" + Seniority2 + "','" + WorkItem3 + "','" + Seniority3 + "','" + WorkItem4 + "' ,'" + Seniority4 + "' ,'" + WorkItem5 + "' ,'" + Seniority5 + "' ,'" + WorkItem6 + "' ,'" + Seniority6 + "' ,'" + WorkItem7 + "' ,'" + Seniority7 + "' ,'" + WorkItem8 + "' ,'" + Seniority8 + "')";
                    string InsertUserOther = "Insert into HUT_UserOther(UserID,RecName1,RecFirm1,RecDep1,RecTitle1,RecTel1,RecName2,RecFirm2,RecDep2,RecTitle2,RecTel2)values('"+User+"','" + RecName1 + "','" + RecFirm1 + "','" + RecDep1 + "','" + RecTitle1 + "','" + RecTel1 + "','" + RecName2 + "','" + RecFirm2 + "' ,'" + RecDep2 + "' ,'" + RecTitle2 + "' ,'" + RecTel2 + "')";
                    string InsertUserSkill = "Insert into HUT_UserItSkill(UserID,MsOffice,Erp,CadCam,Other)values('"+User+"','" + MsOffice + "','" + Erp + "','" + CadCam + "','" + Other + "')";
                    string InsertUserTrBio = "Insert into HUT_UserTrBio(UserID,TrainCreden,ChnBio,EngBio)values('"+User+"','" + TrainCreden + "','" + ChnBio + "','" + EngBio + "')";
                    SqlCommand insertcmd = new SqlCommand(InsertUserFar + " " + InsertUser + " " + InsertUserGuide + " " + InsertUserOther+" "+InsertUserSkill+" "+InsertUserTrBio, cn);
                    insertcmd.ExecuteNonQuery();
                    insertcount=1;
                    
                }
            }
            else
            {
               errorcount=1;
            }  
            //SqlBulkCopy BulkCopy = new SqlBulkCopy(cn);
            //宣告SqlBulkCopy物件 
           // BulkCopy.DestinationTableName = "Test";
            //定義要匯入的資料庫Table 
            //BulkCopy.WriteToServer(ExcelDr);
            //寫入資料 
            if (insertcount == 1)
            {
                Response.Write("<script>alert('"+NameC+"上傳成功')</script>");
            }
            if (updatecount == 1)
            {
                Response.Write("<script>alert('"+NameC +"上傳更新成功')</script>");
            }
            if (errorcount == 1)
            {
                Response.Write("<script>alert('資料上傳失敗')</script>");
            }
            ExcelDr.Dispose();
            ExcelDr.Close();
            ExcelCmd.Dispose();
            ExcelCn.Dispose();
            ExcelCn.Close();
            //BulkCopy.Close();

            cn.Dispose();
            cn.Close();

            //GridView1.DataBind();
            System.IO.File.Delete(path);


            //Response.Write("<script>window.opener =null;window.close()</script>");
        }
    }

    //protected void JQImageContainer1_DataBinding(object sender, EventArgs e)
    //{
    //    if (string.IsNullOrEmpty((sender as Srvtools.WebImage).ImageUrl))
    //        (sender as Srvtools.WebImage).ImageUrl = "JBImage/btn_08.png";
    //}
}


