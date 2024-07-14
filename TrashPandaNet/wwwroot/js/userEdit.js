function updateImage(input) {
    const img = document.getElementsByClassName('profile-pic')[0];

    img.style.backgroundImage = `url(${input.value})`;
}