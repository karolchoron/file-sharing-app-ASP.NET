<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="Lab4.AdminPanel" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <br />
    <asp:SqlDataSource ID="SqlDataNazwyUserow" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [AspNetUsers]"></asp:SqlDataSource>
    <br />
    <asp:SqlDataSource ID="SqlDataROLEUserow" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [AspNetUserRoles]"></asp:SqlDataSource>
    <br />
    Wybierz z listy nazwe użytkownika dla którego chcesz nadać rolę administratora i następnie kliknij przycisk "Nadaj role".
    <br />
    <br />
    <asp:DropDownList ID="DropDownListNAZWYuserow" runat="server" DataSourceID="SqlDataNazwyUserow" DataTextField="Email" DataValueField="Id" OnSelectedIndexChanged="DropDownListNAZWYuserow_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Button ID="NadajRoleButton" runat="server" Text="Nadaj role" Height="27px" OnClick="NadajRoleButton_Click1" />
    <br />
    <br />
    <asp:Label ID="RolaLabel" runat="server" Text=""></asp:Label>

</asp:Content>