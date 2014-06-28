<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserLogin.aspx.cs" Inherits="Views_UserLogin" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-sm-12" style="text-align:center">
        <div class="col-sm-2 panel">                       
            <div class="form-group">
            <label>Username</label>
            <asp:Textbox  id="txtLogin" runat="server" autocomplete="off"/>
                                    
            </div>

            <div class="form-group">
            <label>Password</label>
            <asp:Textbox  id="txtPassword" runat="server" autocomplete="off"/>
                                    
            </div>

            <div class="form-group" style="text-align:right">
            <asp:Button ID="btnLogin" runat="server" class="btn btn-primary" OnClick="btnLogin_Click" Text="Login"/>
            
            
             
            </div>
            <div class="form-group">
                <asp:Label ID="lblError" runat="server" Text=""/>
            </div>
        </div>
    </div>


    </asp:Content>