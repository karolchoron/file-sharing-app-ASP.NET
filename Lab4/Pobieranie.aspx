<%@ Page Title="Pobieranie" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pobieranie.aspx.cs" Inherits="Lab4.About" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <br />
    <br />
    <br />
    Wybierz z listy plik, który chcesz pobrać a następnie kliknij przycisk
    <br />
    <br />
    <asp:DropDownList ID="ListaDropDown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListaDropDown_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Button ID="Pobieranie" runat="server" Text="Pobieranie" OnClick="Pobieranie_Click" />
    <br />
    <br />
</asp:Content>
