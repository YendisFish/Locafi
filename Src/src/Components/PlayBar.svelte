<script>
    import {playSong, pauseSong} from "../audioController.js";
    import {currentSong, isPlaying} from "../globals.js";
    
    let playing = false;
    isPlaying.subscribe(value => {
        playing = value;
    });
    
    async function togglePlay() {
        isPlaying.set(!playing);
        
        try {
            if(playing) {
                await playSong();
            }

            if(!playing) {
                await pauseSong();
            }
        } catch(e) {
            console.log(e);
        }
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
        {#if playing}
        <i on:click="{togglePlay}" class='bx bx-pause-circle bx-md color-green'></i>
        {:else}
        <i on:click="{togglePlay}" class='bx bx-play-circle bx-md color-green'></i>
        {/if}
    </div>    
</div>