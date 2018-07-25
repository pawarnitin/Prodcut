<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Storeproducts._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function getProducts() {
            $.getJSON("api/products",
                function (data) {
                    $('#products').empty(); // Clear the table body.

                    // Loop through the list of products.
                    $.each(data, function (key, val) {
                        // Add a table row for the product.
                        var row = '<td>' + val.Name + '</td><td>' + val.quantity + '</td><td>' + val.sale_amount + '</td>';
                        //var row = '<td>' + val.Name + '</td><td>' + val.quantity + '</td>'< td >'+ val.sale_amount + '</td>';
                        $('<tr/>', { html: row })  // Append the name.
                            .appendTo($('#products'));
                    });
                });
        }

        $(document).ready(getProducts);
</script>
    <h2>Products</h2>
    <table>
        <thead>
            <tr>
                <th>Name</th>
                <th>Quantity</th>
                <th>Sale_amount</th>
            </tr>
        </thead>
        <tbody id="products">
        </tbody>
    </table>
</asp:Content>
