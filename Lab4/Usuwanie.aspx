<%@ Page Title="Usuwanie" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuwanie.aspx.cs" Inherits="Lab4.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    

    <br />
    <br />
    <br />
    <asp:SqlDataSource ID="SqlDataSourceUsuwanieNAZWA" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [AspNetUsers]"></asp:SqlDataSource>
    <br />
    <asp:SqlDataSource ID="SQLPlik" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [Pliki]"></asp:SqlDataSource>
    <br />
    Wybierz plik z listy a następnie kliknij przycisk "Usuń"
    <br />
    <asp:DropDownList ID="DropDownListUsuwanie" runat="server" AutoPostBack="True" DataSourceID="SQLPlik" DataTextField="NazwaPliku" DataValueField="NazwaPliku" OnSelectedIndexChanged="DropDownListUsuwanie_SelectedIndexChanged"></asp:DropDownList>
    <br />
    <asp:Label ID="BrakPlikowError" runat="server" Text=""></asp:Label>
    <br />
    <br />
    <asp:Button ID="ButtonUsun" runat="server" Text="Usuń" OnClick="ButtonUsun_Click" />
    <br />
    <br />
    <asp:Label ID="UsunLabel" runat="server" Text=""></asp:Label>
</asp:Content>
