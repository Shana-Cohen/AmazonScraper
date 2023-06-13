import React, {useState} from 'react';
import axios from 'axios';

const Home = () => {
    const [items, setItems] = useState([]);
    const [searchText, setSearchText] = useState('');

    const onButtonClick = async () => {
        const { data } = await axios.get(`/api/amazon/scrape/${searchText}`);
        setItems(data);
    }

    return (
        <div className='container mt-5'>
            <div className='row'>
                <div className='col-md-10'>
                <input type='text' className='form-control' placeholder='Search Amazon' 
                onChange={e => setSearchText(e.target.value)} value={searchText} />
                </div>
                <div className='col-md-2'>
                    <button className='btn btn-primary btn-block' onClick={onButtonClick}>
                        Search
                    </button>
                </div>
            </div>
            <div className='row mt-3'>
                <div className='col-md-12'>
                {!!items.length && <table className='table table-hover table-striped table-bordered'>
                    <thead>
                        <tr>
                            <th>Image</th>
                            <th>Title</th>
                            <th>Price</th>
                        </tr>
                    </thead>
                    <tbody>
                        {items.map((item, idx) => {
                            return <tr key={idx}>
                                <td><img src={item.imageUrl} /></td>
                                <td>
                                    <a href={item.link} target='_blank'>{item.title}</a>
                                </td>
                                <td>
                                    {item.price}
                                </td>
                            </tr>
                        })}
                    </tbody>
                    </table>}
                    </div>
            </div>
        </div>
    )
}

export default Home;