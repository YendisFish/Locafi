<script lang="ts">
    let isPlaying = false;
    
    function togglePlay() {
        isPlaying = !isPlaying;
    }
</script>

<style>
    .main-bar {
        background: black;
        width: 100vw; /* Ensure the div takes up the full viewport width */
        display: flex; /* Use flexbox for centering */
        justify-content: center; /* Center content horizontally */
        align-items: center; /* Center content vertically */
    }
    
    .color-green {
        color: red;
    }
</style>

<div class="main-bar">
    <link href="./node_modules/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
    
    <div style="padding: 5px">
    {#if isPlaying}
        <i on:click="{togglePlay}" class='bx bx-pause-circle bx-md color-green'></i>
    {:else}
        <i on:click="{togglePlay}" class='bx bx-play-circle bx-md color-green'></i>
    {/if}
    </div>    
</div>