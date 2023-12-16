if (!localStorage.getItem('userID') || localStorage.getItem('userID')=='undefined') {
    window.location.href = 'login.html';
}