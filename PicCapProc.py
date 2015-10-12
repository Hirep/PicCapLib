import urllib.request
import re
import datetime

__author__ = 'hirep'


class PicCap:

    def get_page_src(link):
        # Return HTML-source of link
        page = urllib.request.urlopen(link)
        # Decoded string of source
        # Using 'latin-1' codec because
        # of byte '0xd6' which cannot be decoded by 'utf-8' (APOD)
        source = page.read().decode('latin-1')
        return source

    def get_image_link(link, rx_pattern):
        # Obtain html
        s = PicCap.get_page_src(link)
        # Match image link
        s = re.search(rx_pattern, s)
        # Compile absolute path
        image_link = link + s.group(1)
        return image_link

    def save_image(image_link, name):
        # Download image from given link
        current_date = datetime.datetime.now()
        # Filename format "name-dd-mm-yyyy.jpg"
        page = urllib.request.urlretrieve(image_link,  name + '-'
                                                        '{0:02.0f}'.format(current_date.day) + '-' +
                                                        '{0:02.0f}'.format(current_date.month) + '-' +
                                                        '{0:02.0f}'.format(current_date.year) + '.jpg')

    def get_image(link, rx_pattern, name):
        PicCap.save_image(
            PicCap.get_image_link(
                link,
                rx_pattern
            ),
            name
        )
       


class APOD:
    """
    APOD (Astronomy Picture of the Day)
    Each day a different image or photograph
    of our fascinating universe is featured
    """
    link = "http://apod.nasa.gov/apod/"
    rx_pattern = '<a href="(image.*?)"'
    name = 'NASA'

    def get_image(self):
        PicCap.get_image(APOD.link,APOD.rx_pattern, APOD.name)


class Bing:
    """
    Daily changing of background image.
    The images are mostly of noteworthy places in the world,
    though it sometimes displays animals, people, and sports.
    """
    link = 'http://bing.com/'
    rx_pattern = "g_img={url:'/(.*?)'"
    name = 'BING'

    def get_image(self):
        PicCap.get_image(Bing.link, Bing.rx_pattern, Bing.name)


