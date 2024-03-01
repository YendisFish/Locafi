function handleIncoming(message) {
    switch(message) {
        //add cases later
    }
}

function sendMessage(obj) {
    window.external.sendMessage(JSON.stringify(obj));
}