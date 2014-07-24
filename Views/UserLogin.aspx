 <%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserLogin.aspx.cs" Inherits="Views_UserLogin" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3 style="cursor:pointer" onclick="gettasks()">Login</h3>
    <div class="row" style="margin-top:30px; ">
        <div class="col-lg-12" >                     
            <div class="form-group">
            <label>Username</label>
            <asp:Textbox  id="txtLogin" runat="server" autocomplete="off"/>
                                    
            </div>

            <div class="form-group">
            <label>Password</label>

            <asp:Textbox  id="txtPassword" type="password" runat ="server" autocomplete="off"/>
                                    
            </div>
            <br />
            <div class="form-group">
            <asp:Button ID="btnLogin" runat="server" class="btn btn-primary" OnClick="btnLogin_Click" Text="Login"/>
            
            
             
            </div>
            
            <div class="form-group">
                <asp:Label ID="lblError" runat="server" Text=""/>
            </div>
        </div>
    </div>


    </asp:Content>