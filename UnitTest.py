import requests

# Test get all tasks with limit
def get_all_tasks():
    limit = 10

    response = requests.get(f'http://localhost:7262/getAllTasks?limit={limit}&offset=0')
    assert response.status_code == 200

    objects = response.json()
    # Check that the limit actually worked
    assert len(objects['todos']) <= limit
    
    # Check that the total parameter is working
    assert len(objects['todos']) <= objects['total']

    # Return the id of the first task
    return objects['todos'][0]['id']

# Test get all tasks with offset
def get_all_tasks_with_offset(first_task_id):
    offset = 10

    response = requests.get(f'http://localhost:7262/getAllTasks?limit=10&offset={offset}')
    # Check response status code
    assert response.status_code == 200

    objects = response.json()
    # Check that the offset actually worked
    assert objects['todos'][0]['id'] != first_task_id

# Test get tasks by user with limit
def get_tasks_by_user():
    limit = 150
    user_id = 3

    response = requests.get(f'http://localhost:7262/getTasksByUser?userId={user_id}&limit={limit}&offset=0')
    # Check response status code
    assert response.status_code == 200

    objects = response.json()
    assert len(objects['todos']) <= limit
    for task in objects['todos']:
        assert task['userId'] == user_id

def get_all_users():
    limit = 5

    response = requests.get(f'http://localhost:7262/getAllUsers?limit={limit}')
    # Check response status code
    assert response.status_code == 200

    objects = response.json()
    # Check that the limit actually worked
    assert len(objects['users']) <= limit

    # Check that the total parameter is working
    assert len(objects['users']) <= objects['total']



task_id = get_all_tasks()
get_all_tasks_with_offset(task_id)
get_tasks_by_user()
get_all_users()
print("All tests passed!")