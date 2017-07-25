
Reference: https://www.youtube.com/watch?v=rMA69bVv0U8&t=6s

Use postman plugin of chrome to make the API call

http://localhost:24189/token
Method: POST
Body:
    x-www-form-urlencoded
    username=#username#&password=#password#&grant_type=password
    
Response:

{
    "access_token": "UhGHeid2R0u7TDPGgpil1dCMo-4_HQuHprqgyDykNQiqrIzErUdJ65dYO-FhpWSiejEv3wLi9OedJ4QfRnKlwuw--xo8MP6OPeEUPJ6z22htexYzUvoWYiuSQcz-HYiOQACJplnXKDCwVjMPvtkliTR-KTRwZS169E2C6Ab6VNysC6w52yt9WcWcmaRtbBQOsHoMK0l8DZl9kTTyCPdgXquo0Kv8s56HhK-H7RDhXpmQ2_WgtjOE4Xm1oCvRj80yM247F-NNkQDppJgIJ0x-771lwUTi_3I7bR-dL808TSXT0L84_zZ1oyxlIktPy2CW",
    "token_type": "bearer",
    "expires_in": 86399
}	


http://localhost:24189/api/data/forauthorizeduser
Method: GET
Header: Authorization bearer #token#


http://localhost:24189/api/data/forauthorizeduser
Method: GET
Header: Authorization bearer #token#
