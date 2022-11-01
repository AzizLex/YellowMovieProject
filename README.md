# Yellow Movie Project

This Movie Shop project is created by Group12 Yellow Team - Lexicon Linköping.
<ul>
<li>Aziz Mannanov</li>
<li>Emil Lindmark</li>
<li>Ragnar Freudethal</li>
<li>Raid Mahbouba</li>
</ul>

<h4>Training opportunities</h4>
<ul>
<li>Bootstrap 5 - opportunity to use feature of "cards".</li>
<li>ASP.Net Framework 4.8.</li>
<li>Using session, not cookies.</li>
<li>Continuous integration and deployment, without pipelining.</li>
<li>Daily updates to repository on GitHub.</li>
<li>Published on Azure.</li>
</ul>

<h4>Index page showing </h4>
<ul>
<li>The most popular movies based on the number of orders made.</li>
<li>The 5 newest movies (based on release year).</li>
<li>The 5 oldest movies (based to release year).</li>
<li>The 5 cheapest movies (based to price).</li>
<li>The customer who has made the most expensive order (based of the sum of the order rows in an order).</li>
</ul>

<h4>A page displaying all movies in the database </h4>
<ul>
<li>Each entry(movie) displays Title, Director, Release Year and Price.</li>
<li>Each entry (movie) rendered using a partial view.</li>
<li>A button next to each movie exists, allowing the user to add the movie to the order cart; using session.</li>
<li>If the user clicks on “adding to cart” twice on the same movie, two copies are added to the order cart.</li>
</ul>

<h4>A page where user can add a movie to the database </h4>
<ul>
<li>A form to add the movie by entering the Title, Director, Release Year and Price.</li>
<li>Add an image URL to the movies table.</li>
</ul>

<h4>Display shopping cart </h4>
<ul>
<li>A page that displays all the items in the shopping cart allowing the user to check out (place an order). When the user checks out, the order should be created for the user with the given email address. There are two options: if the user has registered before - email address is entered, if not - the user registers by adding all necessary information (columns from customers table).</li>
<li>Two buttons, add and remove copy buttons, that adds and reduces the number of copies for a movie by one.</li>
<li>After order completion the user is redirected to a page saying that the order was successfully completed.</li>
<li>When a customer registers an order an e-mail is sent by using Azure functions.</li>
</ul>

<h4>Displays orders for a customer </h4>
<ul>
<li>Display all orders for a given customer, include total count of orders.</li>
<li>Each order for the user is displayed together with the movies belonging to the order (order rows). Also, the total cost for the order should be displayed.</li>
<li>Each order is rendered using a partial view.</li>
</ul>

<h4>Optional items performed </h4>
<ul>
<li>Pagination.</li>
<li>Search bar.</li>
<li>Allow customer to add separate billing address.</li>
</ul>

<h4>Optional items not performed:</h4>
<ul>
<li>Account functionality (login).</li>
<li>We did not use join or include in LINQ-queries.</li>
<li>Using other solution than URL looking something like /Customers/Orders/{Email Address}</li>
</ul>
