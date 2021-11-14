var stripeKey = "";
var stripe, customer, price, card;
function initializeStripe(apiKey) {
    console.log(apiKey)
    stripe = window.Stripe(apiKey);
   
    console.log(card);
}
function initiateCard() {
    var elements = stripe.elements();
    var cardElement = elements.create('card');
    cardElement.mount('#card-element');
}