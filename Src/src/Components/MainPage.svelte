<script lang="ts">
    let inSettings = false;
    let currentPlaylist = "";
    let songList = [];
    
    function toggleSettings() {
        inSettings = !inSettings;
    }
    
    function setAndGetPlaylist(playlist) {
        currentPlaylist = playlist;
    }
</script>

<style>
    .main-page {
        background: #2b2f32;
        height: 100vh;
        width: 60vw;
        display: flex; /* Use flexbox for centering */
        justify-content: center; /* Center content horizontally */
        align-items: center; /* Center content vertically */
        margin: 0 auto; /* Center the container horizontally */
    }
</style>

<div class="main-page">
    <link href="./node_modules/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
</div>