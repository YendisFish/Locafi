<script>
    import {playSong, pauseSong, setVolume, loadSong, stopSong} from "../audioController.js";
    import {currentSong, isPlaying, currentSongList, currentTime} from "../globals.js";
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

    let selectedSong = null;
    currentSong.subscribe(value => {
        selectedSong = value;
    });
    
    let selectedSongList = null;
    currentSongList.subscribe(value => {
       selectedSongList = value; 
    });

    async function setCurrentSong(song) {
        if(!selectedSong) {
            currentSong.set(song);
            await loadSong();
            await playSong();
            await setVolume(0.25);
        }

        if(!playing && (song != selectedSong)) {
            currentSong.set(song);
            await stopSong();

            await loadSong();
            await playSong();
            await setVolume(0.25);
        }

        if(song != selectedSong) {
            currentSong.set(song);
            await stopSong();

            await loadSong();
            await playSong();
            await setVolume(0.25);
        }
    }

    async function skipCurrent() {
        console.log(selectedSong.name);

        currentTime.set(0);

        let currentIndex = selectedSongList.findIndex(song => song.name === selectedSong.name);
        await setCurrentSong(selectedSongList[currentIndex + 1]);
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

    <button on:click={skipCurrent}>Skip</button>
    
    <div class="volume-meter">
        <input type="range" min="0" max="1" step="0.01" style="margin-left: 20vw; width: 200px" bind:value={cVol} on:change={updateVolume} />
    </div>
</div>