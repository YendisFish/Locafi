<script>
    import {playSong, pauseSong, setVolume} from "../audioController.js";
    import {currentSong, isPlaying} from "../globals.js";
    import {currentVolume} from "../globals.js";

    let playing = false;
    isPlaying.subscribe(value => {
        playing = value;
    });

    let cVol = null;
    currentVolume.subscribe(value => {
        cVol = value;
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

    async function updateVolume(e) {
        await setVolume(e.target.valueAsNumber);
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
    
    .volume-meter {
        position: fixed;
        bottom: 0;
        right: 5vw;
        padding: 10px;
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

    <div class="volume-meter">
        <input type="range" min="0" max="1" step="0.01" style="margin-left: 20vw; width: 200px" bind:value={cVol} on:change={updateVolume} />
    </div>
</div>